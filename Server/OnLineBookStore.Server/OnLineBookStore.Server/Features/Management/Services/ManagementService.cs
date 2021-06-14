namespace OnLineBookStore.Server.Features.Management.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using OnLineBookStore.Server.Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ManagementService : IManagementService
    {
        private readonly OnLineBookStoreDbContext _data;

        public ManagementService(OnLineBookStoreDbContext data)
        {
            _data = data;
        }

        public async Task<bool> SetQuantityThreshold(string id, int quantity)
        {
           var inventory = await _data.Inventories.FirstOrDefaultAsync(x => x.Id == id);
           inventory.QantityLimitTreshhold = quantity;

           _data.Inventories.Update(inventory);
           return await _data.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateInventory(string id)
        {
            var inventory = new Inventory
            {
                Id = id + "1",
                QantityLimitTreshhold = 0
            };
            _data.Inventories.Add(inventory);
            return await _data.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<BookForInventoryViewModel>> GetInventory() =>
          await _data.Books
              .OrderBy(b => b.Quantity)
              .Select(b => new BookForInventoryViewModel
              {
                  Author = b.Author,
                  Id = b.Id,
                  ISBN = b.ISBN,
                  NumberOfPurchases = b.NumberOfPurchases,
                  NumberOfPages = b.NumberOfPages,
                  Price = b.Price,
                  Quantity = b.Quantity,
                  Title = b.Title,
                  Year = b.Year,
                  QuantityLimit = b.QuantityLimit,
              }).ToListAsync();

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
            int quantityLimit,
            string creatorId)
        {

            var book = await _data.Books.FirstOrDefaultAsync(x => x.CreatorId == creatorId && x.Id == id);

            if (book == null) return false;
            if (quantity > quantityLimit) return false;

            book.Description = description;
            book.SummaryDescription = summaryDescription;
            book.BookImage = bookImage;
            book.Price = price;
            book.Quantity = quantity;
            book.CreatorId = creatorId;
            book.QuantityLimit = quantityLimit;

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
            int quantityLimit,
            string creatorId)
        {
            if (quantity > quantityLimit) return null;
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
                CreatorId = creatorId,
                NumberOfPurchases = numberOfPurchases,
                QuantityLimit = quantityLimit
            };

            _data.Books.Add(book);
            await _data.SaveChangesAsync();

            return new CreateBookResponseModel { BookId = book.Id };
        }
    }
}
