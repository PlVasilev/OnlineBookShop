using System.ComponentModel.DataAnnotations;

namespace OnLineBookStore.Server.Features.Identity.Models
{
    public class RegisterUserRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
