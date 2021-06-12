using System.Linq;

namespace OnLineBookStore.Server.Features.Cart.Services
{
    using System;
    using System.Threading.Tasks;
    using Data;
    public class CartService : ICartService
    {
        private readonly OnLineBookStoreDbContext _data;

        public CartService(OnLineBookStoreDbContext data)
        {
            _data = data;
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
