namespace OnLineBookStore.Server.Features.Book.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    public interface IBookService
    {
        public Task<IEnumerable<BookListViewModel>> BestSellers();
    }
}
