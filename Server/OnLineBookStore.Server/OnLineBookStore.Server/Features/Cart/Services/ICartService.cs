namespace OnLineBookStore.Server.Features.Cart.Services
{
    using System.Collections.Generic;
    using Models;
    using System.Threading.Tasks;
    public interface ICartService
    {
        public Task<bool> AddToUser(string userId);

        public Task<IEnumerable<CartBooksViewModel>> GetCart(string userId);
        public Task<bool> AddToCart(string userId, string bookId, int quantity);
    }
}
