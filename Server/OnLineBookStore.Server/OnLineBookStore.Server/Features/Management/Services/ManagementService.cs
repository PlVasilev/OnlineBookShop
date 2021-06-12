namespace OnLineBookStore.Server.Features.Management.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using OnLineBookStore.Server.Data.Models;
    using Models;

    public class ManagementService : IManagementService
    {
        private readonly OnLineBookStoreDbContext _data;

        public ManagementService(OnLineBookStoreDbContext data)
        {
            _data = data;
        }

        public async Task<bool> Delete(string id, string userId)
        {

            var book = await _data.Books.FirstOrDefaultAsync(x => x.Id == id && x.CreatorId == userId);
            if (book == null) return false;

            _data.Remove(book);
            await _data.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(
            string id,
            string description,
            string summaryDescription,
            string bookImage,
            decimal price,
            int quantity,
            int numberOfPurchases,
            string creatorId)
        {

            var book = await _data.Books.FirstOrDefaultAsync(x => x.CreatorId == creatorId && x.Id == id);

            if (book == null) return false;

            book.Description = description;
            book.SummaryDescription = summaryDescription;
            book.BookImage = bookImage;
            book.Price = price;
            book.Quantity = quantity;
            book.NumberOfPages = numberOfPurchases;
            book.CreatorId = creatorId;

            await _data.SaveChangesAsync();
            return true;
        }

        public async Task<CreateBookResponseModel> Create(
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
            string creatorId)
        {
            var book = new Book
            {
                Id = Guid.NewGuid().ToString(),
                Title = title,
                Description = description,
                SummaryDescription = summaryDescription,
                ISBN = ISBN,
                BookImage = bookImage,
                Author = author,
                Year = year,
                Price = price,
                Quantity = quantity,
                NumberOfPages = numberOfPages,
                CreatorId = creatorId
            };

            _data.Books.Add(book);
            await _data.SaveChangesAsync();

            return new CreateBookResponseModel{BookId = book.Id};
        }
    }
}
