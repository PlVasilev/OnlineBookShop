namespace OnLineBookStore.Server.Features.Cart
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Infrastructure;

    [Authorize]
    public class CartController : ApiController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpGet]
        [Route(nameof(GetCart))]
        public async Task<IEnumerable<CartBooksViewModel>> GetCart()
        {
            var userId = User.GetId();
            return await _cartService.GetCart(userId);
        }
            

        [HttpPost]
        [Route(nameof(AddToCart))]
        public async Task<ActionResult> AddToCart(AddToCartRequestModel model)
        {
            var userId = User.GetId();
            var result = await _cartService.AddToCart(userId, model.BookId, model.Quantity);

            if (!result)
                BadRequest();
            
            return Ok();
        }

        [HttpPost]
        [Route(nameof(CheckOut))]
        public async Task<ActionResult> CheckOut(IEnumerable<BookCheckOutRequestModel> books)
        {
            var result =  await _cartService.CheckOutBooks(books);
            if (!result)
                BadRequest();

            return Ok();
        }
    }
}
