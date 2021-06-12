using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnLineBookStore.Server.Data;
using OnLineBookStore.Server.Features.Book.Models;

namespace OnLineBookStore.Server.Features.Book.Services
{
    public class BookService : IBookService
    {
        private readonly OnLineBookStoreDbContext _data;
        public async Task<IEnumerable<BookListViewModel>> BestSellers() =>
            await _data.Books.OrderBy(b => b.NumberOfPurchases).Take(6).Select(b => new BookListViewModel
            {
                Title = b.Title,
                Author = b.Author,
                BookImage = b.BookImage,
                IsLimited = b.Quantity < 10,
            }).Take(6).ToListAsync();
    }
}
