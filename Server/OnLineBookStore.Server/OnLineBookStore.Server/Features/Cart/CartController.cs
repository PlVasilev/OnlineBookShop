using OnLineBookStore.Server.Infrastructure;

namespace OnLineBookStore.Server.Features.Cart
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Authorize]
    public class CartController : ApiController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpGet]
        [Route(nameof(GetCart) + "/{id}")]
        public async Task<IEnumerable<CartBooksViewModel>> GetCart(string id) => 
            await _cartService.GetCart(id);


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
    }
}
