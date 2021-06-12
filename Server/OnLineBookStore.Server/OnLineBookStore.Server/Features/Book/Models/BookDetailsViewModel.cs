namespace OnLineBookStore.Server.Features.Book.Models
{
    public class BookDetailsViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string BookImage { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public int Quantity { get; set; }
        public int NumberOfPurchases { get; set; }
    }
}
