namespace OnLineBookStore.Server.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Cart
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        
        [NotMapped]
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
