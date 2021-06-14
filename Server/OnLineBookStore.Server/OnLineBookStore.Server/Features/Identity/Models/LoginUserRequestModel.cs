using System.ComponentModel.DataAnnotations;

namespace OnLineBookStore.Server.Features.Identity.Models
{
    public class LoginUserRequestModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9-]{3,}$")]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9-]{3,}$")]
        public string Password { get; set; }
    }
}
