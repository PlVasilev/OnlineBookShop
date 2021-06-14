using OnLineBookStore.Server.Infrastructure;

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
        [Authorize]
        [Route(nameof(All))]
        public async Task<IEnumerable<BookListViewModel>> All()
        {
            var userId = User.GetId();
            return await _bookService.All(userId);
        }
            

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<BookDetailsViewModel>> Details(string id) =>
            await _bookService.Details(id);

    }
}
