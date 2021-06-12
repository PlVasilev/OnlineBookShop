namespace OnLineBookStore.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    public class User : IdentityUser
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9-]{3,}$")]
        public override string UserName { get; set; }

        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
