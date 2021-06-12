using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnLineBookStore.Server.Features.Management.Models;

namespace OnLineBookStore.Server.Features.Management.Services
{
    public interface IManagementService
    {
        public  Task<bool> Delete(string id, string userId);

        public  Task<bool> Update(
            string id,
            string description,
            string summaryDescription,
            string bookImage,
            decimal price,
            int quantity,
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
            string creatorId);
    }
}
