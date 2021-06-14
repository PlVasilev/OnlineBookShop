namespace OnLineBookStore.Server.Features.Book.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Models;
    public class BookService : IBookService
    {
        private readonly OnLineBookStoreDbContext _data;

        public BookService(OnLineBookStoreDbContext data)
        {
            _data = data;
        }

        public async Task<IEnumerable<BookListViewModel>> All(string userId)
        {
            var inventory = _data.Inventories.FirstOrDefault(i => i.Id == "admin");
            return await _data.Books
                .OrderByDescending(b => b.NumberOfPurchases)
                .Select(b => new BookListViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    BookImage = b.BookImage,
                    Price = b.Price,
                    SummaryDescription = b.SummaryDescription,
                    Quantity = b.Quantity,
                    NumberOfPurchases = b.NumberOfPurchases,
                    IsLimited = b.Quantity < inventory.QantityLimitTreshhold,
                }).ToListAsync();
        }


        public async Task<BookDetailsViewModel> Details(string id)
        {
            var book = await _data.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return null;
            }
            var viewModel = new BookDetailsViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                SummaryDescription = book.SummaryDescription,
                ISBN = book.ISBN,
                BookImage = book.BookImage,
                Author = book.Author,
                Year = book.Year,
                Price = book.Price,
                Quantity = book.Quantity,
                NumberOfPages = book.NumberOfPages,
                NumberOfPurchases = book.NumberOfPurchases,
                QuantityLimit = book.QuantityLimit
            };
            return viewModel;
        }
    }
}
