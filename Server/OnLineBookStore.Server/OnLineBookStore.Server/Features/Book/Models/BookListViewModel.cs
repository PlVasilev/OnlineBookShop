namespace OnLineBookStore.Server.Features.Book.Models
{
    public class BookListViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string BookImage { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string SummaryDescription { get; set; }
        public int Quantity { get; set; }
        public int NumberOfPurchases { get; set; }
        public bool IsLimited { get; set; }
    }
}
