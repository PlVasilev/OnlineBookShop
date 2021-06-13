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

        public async Task<IEnumerable<CartBooksViewModel>> GetCart(string userId)
        {
            var userWithCart = await GetCartContent(userId);
            var cartContent = userWithCart.Cart.CartBooks.ToHashSet();

            var result = new HashSet<CartBooksViewModel>();
            foreach (var cartBook in cartContent)
            {
                result.Add(new CartBooksViewModel
                {
                    CartId = cartBook.CartId,
                    BookId = cartBook.BookId,
                    Title = cartBook.Book.Title,
                    Author = cartBook.Book.Author,
                    Price = cartBook.Book.Price,
                    TotalPrice = cartBook.Book.Price * cartBook.Quantity,
                    Quantity = cartBook.Quantity
                });
            }
            return result;
        }

        public async Task<bool> AddToCart(string userId, string bookId, int quantity)
        {
            var userWithCart = await GetUserCart(userId);
            var cartContent = userWithCart.Cart.CartBooks.ToHashSet();

            var cartBook = cartContent.FirstOrDefault(b => b.BookId == bookId);
            if (cartBook == null)
            { 
                cartBook = new CartBook
                {
                    BookId = bookId,
                    CartId = userWithCart.CartId,
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

        public async Task<bool> CheckOutBooks(IEnumerable<BookCheckOutRequestModel> booksToCheckOut)
        {
            var bookIdQuantityDict = new Dictionary<string, int>();
            foreach (var bookCheckOut in booksToCheckOut)
            {
                bookIdQuantityDict[bookCheckOut.BookId] = bookCheckOut.Quantity;
            }
            var books =  _data.Books.Where(t => bookIdQuantityDict.Keys.Contains(t.Id)).ToList();
            foreach (var book in books)
            {
                book.Quantity -= bookIdQuantityDict[book.Id];
                book.NumberOfPurchases += bookIdQuantityDict[book.Id];
            }

            var cartId = booksToCheckOut.First().CartId;
            ClearCart(cartId);

            _data.Books.UpdateRange(books);
            var result = await _data.SaveChangesAsync();
            return result > 0;
        }


        public async Task<bool> AddToUser(string userId)
        {
            var cart = new Cart
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

        private async Task<User> GetUserCart(string userId) =>
            await _data.Users
                .Include(u => u.Cart)
                .ThenInclude(cb => cb.CartBooks)
                .FirstOrDefaultAsync(u => u.Id == userId);

        private async Task<User> GetCartContent(string userId) =>
            await _data.Users
                .Include(u => u.Cart)
                .ThenInclude(cb => cb.CartBooks)
                .ThenInclude(b => b.Book)
                .FirstOrDefaultAsync(u => u.Id == userId);

        private async void ClearCart(string cartId)
        {
            var cart = await _data.Carts
                .Include(c => c.CartBooks)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            _data.CartBooks.RemoveRange(cart.CartBooks);
        }
    }
}
