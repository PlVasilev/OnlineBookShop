﻿namespace OnLineBookStore.Server.Features.Cart.Models
{
    public class CartBooksViewModel
    {
        public string CartId { get; set; }
        public string BookId { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
    }
}

