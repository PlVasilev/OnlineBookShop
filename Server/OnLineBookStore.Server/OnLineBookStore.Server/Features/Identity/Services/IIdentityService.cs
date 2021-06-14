namespace OnLineBookStore.Server.Features.Identity.Services
{
    using System.Collections.Generic;
    using Models;
    public interface IIdentityService
    {
        public LoginResponseModel GenerateJwtToken(string userId, string userName, string appSecret, IEnumerable<string> roles);
    }
}
