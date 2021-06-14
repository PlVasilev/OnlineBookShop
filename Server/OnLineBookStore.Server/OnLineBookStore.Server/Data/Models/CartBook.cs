namespace OnLineBookStore.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class CartBook
    {
        [Required]
        public string CartId { get; set; }
        public Cart Cart { get; set; }

        [Required]
        public string BookId { get; set; }
        public Book Book { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
