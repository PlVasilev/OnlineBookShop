namespace OnLineBookStore.Server.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Cart
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<CartBook> CartBooks { get; set; } = new List<CartBook>();
    }
}
