using System.Collections.Generic;
using OnLineBookStore.Server.Features.Identity.Models;

namespace OnLineBookStore.Server.Features.Identity.Services
{
    public interface IIdentityService
    {
        public LoginResponseModel GenerateJwtToken(string userId, string userName, string appSecret, IEnumerable<string> roles);
    }
}
