namespace OnLineBookStore.Server.Features.Book.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    public interface IBookService
    {
        public Task<IEnumerable<BookListViewModel>> All(string userId);
        public Task<BookDetailsViewModel> Details(string id);
    }
}
