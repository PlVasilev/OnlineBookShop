using System.Collections.Generic;

namespace OnLineBookStore.Server.Features.Cart.Models
{
    public class CartViewModel
    {
        public string CartId { get; set; }

        List<CartBooksViewModel> Books { get; set; }
        
    }
}
