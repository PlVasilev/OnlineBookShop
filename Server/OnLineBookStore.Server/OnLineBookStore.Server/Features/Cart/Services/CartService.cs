namespace OnLineBookStore.Server.Features.Cart.Services
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using OnLineBookStore.Server.Data.Models;

    public class CartService : ICartService
    {
        private readonly OnLineBookStoreDbContext _data;

        public CartService(OnLineBookStoreDbContext data)
        {
            _data = data;
        }

        public async Task<IEnumerable<CartBooksViewModel>> GetCart(string id)
            => await _data.Carts
                .Where(cart => cart.Id == id)
                .Include(cartBook => cartBook.CartBooks)
                .ThenInclude(book => book.Book)
                .SelectMany(c => c.CartBooks, (c, cb) =>
                    new CartBooksViewModel
                    {
                        CartId = cb.CartId,
                        BookId = cb.BookId,
                        BookTitle = cb.Book.Title,
                        Quantity = cb.Quantity
                    }
                ).ToListAsync();

        public async Task<bool> AddToCart(string userId, string bookId, int quantity)
        {
            var user =  await _data.Users
                .Include(u => u.Cart)
                .ThenInclude(cb => cb.CartBooks)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var cartContent = user.Cart.CartBooks.ToHashSet();

            var cartBook = cartContent.FirstOrDefault(b => b.BookId == bookId);
            if (cartBook == null)
            { 
                cartBook = new CartBook
                {
                    BookId = bookId,
                    CartId = user.CartId,
                    Quantity = quantity
                };
                _data.CartBooks.Add(cartBook);
            }
            else
            {
                cartBook.Quantity += quantity;
                _data.CartBooks.Update(cartBook);
            }
            return await _data.SaveChangesAsync() > 0;
        }


        public async Task<bool> AddToUser(string userId)
        {
            var cart = new Data.Models.Cart
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId
            };

            _data.Carts.Add(cart);
            var user = _data.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return false;
            
            user.CartId = cart.Id;

            _data.Users.Update(user);
            
            var result = await _data.SaveChangesAsync();
            return result > 0;
        }
    }
}
