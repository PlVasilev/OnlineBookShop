namespace OnLineBookStore.Server.Features.Cart.Services
{
    using System.Threading.Tasks;
    public interface ICartService
    {
        public Task<bool> AddToUser(string userId);
    }
}
