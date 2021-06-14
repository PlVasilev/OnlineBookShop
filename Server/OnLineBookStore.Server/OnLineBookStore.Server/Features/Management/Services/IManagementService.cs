namespace OnLineBookStore.Server.Features.Management.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    public interface IManagementService
    {
        public  Task<bool> Delete(string id, string userId);
        public Task<bool> SetQuantityThreshold(string id, int quantity);

        public Task<bool> CreateInventory(string id);
        public  Task<IEnumerable<BookForInventoryViewModel>> GetInventory();

        public  Task<bool> Update(
            string id,
            string description,
            string summaryDescription,
            string bookImage,
            decimal price,
            int quantity,
            int quantityLimit,
            string creatorId);

        public  Task<CreateBookResponseModel> Create(
            string title, 
            string description, 
            string summaryDescription,
            string ISBN,
            string bookImage,
            string author,
            int year,
            decimal price,
            int numberOfPages,
            int quantity,
            int numberOfPurchases,
            int quantityLimit,
            string creatorId);
    }
}
