namespace OnLineBookStore.Server.Features.Cart.Models
{
    using System.ComponentModel.DataAnnotations;
    public class AddToCartRequestModel
    {
        [Required]
        public string BookId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

    }
}
