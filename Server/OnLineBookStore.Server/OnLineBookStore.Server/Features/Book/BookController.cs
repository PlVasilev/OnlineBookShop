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
        [Route(nameof(All))]
        public async Task<IEnumerable<BookListViewModel>> All() =>
            await _bookService.All();

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<BookDetailsViewModel>> Details(string id) =>
            await _bookService.Details(id);

    }
}
