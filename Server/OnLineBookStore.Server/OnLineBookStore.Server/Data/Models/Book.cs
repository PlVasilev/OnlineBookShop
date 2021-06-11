using System.Collections.Generic;

namespace OnLineBookStore.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Book
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string SummaryDescription { get; set; }

        [Required]
        [RegularExpression("[0-9-]{13}")]
        public string ISBN { get; set; }

        [Required]
        [Url]
        public string BookImage { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Author { get; set; }

        [Required]
        [RegularExpression("^[1|2]+[0-9]{3}$")]
        public string Year { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
      
        [Range(1, int.MaxValue)]
        public int NumberOfPages { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue)]
        public int NumberOfPurchases { get; set; }

        public ICollection<CartBook> CartBooks { get; set; } = new List<CartBook>();

    }
}
