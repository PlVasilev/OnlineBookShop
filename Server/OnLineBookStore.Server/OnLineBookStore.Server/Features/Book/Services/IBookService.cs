namespace OnLineBookStore.Server.Features.Book.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    public interface IBookService
    {
        public Task<IEnumerable<BookListViewModel>> All();
        public Task<BookDetailsViewModel> Details(string id);
        public Task<bool> Delete(string id, string userId);
    }
}
