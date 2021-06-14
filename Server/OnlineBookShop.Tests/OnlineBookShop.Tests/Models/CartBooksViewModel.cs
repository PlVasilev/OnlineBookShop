﻿namespace OnlineBookShop.Tests.Models
{
    public class CartBooksViewModel
    {
        public string CartId { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}

