using System.ComponentModel.DataAnnotations;

namespace OnLineBookStore.Server.Features.Identity.Models
{
    public class RegisterUserRequestModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9-]{3,}$")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9-]{3,}$")]
        public string Password { get; set; }
    }
}
