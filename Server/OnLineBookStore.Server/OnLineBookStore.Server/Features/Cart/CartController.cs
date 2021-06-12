namespace OnLineBookStore.Server.Features.Cart
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Models;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CartController : Controller
    {
        private readonly OnLineBookStoreDbContext _data;

        public CartController(OnLineBookStoreDbContext data)
        {
            _data = data;
        }

        [HttpGet]
        [Route(nameof(GetCart) + "/{id}")]
        public async Task<ActionResult<IEnumerable<CartBooksViewModel>>> GetCart(string id)
        {
            var result = await _data.Carts
                .Where(cart => cart.Id == id)
                .Include(cartBook => cartBook.CartBooks)
                .ThenInclude(book => book.Book)
                .SelectMany(c => c.CartBooks, (c, cb) =>
                    new CartBooksViewModel
                    {
                        BookId = cb.BookId,
                        BookTitle = cb.Book.Title,
                        Quantity = cb.Quantity
                    }
                ).ToListAsync();

            return result;
        }
    }
}
