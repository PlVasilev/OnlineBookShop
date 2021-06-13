namespace OnLineBookStore.Server.Features.Management.Models
{
    using System.ComponentModel.DataAnnotations;
    public class UpdateBookRequestModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string SummaryDescription { get; set; }

        [Required]
        [Url]
        public string BookImage { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantityLimit { get; set; }

    }
}
