namespace OnLineBookStore.Server.Features.Book
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class BookController : ApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(BestSellers))]
        public async Task<IEnumerable<BookListViewModel>> BestSellers() =>
            await _bookService.BestSellers();

    }
}
