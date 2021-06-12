namespace OnLineBookStore.Server.Features.Identity
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using OnLineBookStore.Server.Data.Models;
    using Models;
    using Services;
    using OnLineBookStore.Server.Features.Cart.Services;
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IIdentityService _identityService;
        private readonly ICartService _cartService;


        public IdentityController(UserManager<User> userManager,
            IOptions<AppSettings> appSettings, 
            IIdentityService identityService, 
            ICartService cartService)
        {
            _userManager = userManager;
            _identityService = identityService;
            _appSettings = appSettings.Value;
            _cartService = cartService;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserRequestModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email
            };

            if (await _userManager.FindByEmailAsync(user.Email) != null)
                return BadRequest("User with that Email Exists.");
            
            if (await _userManager.FindByNameAsync(user.UserName) != null)
                return BadRequest("User with that Username Exists.");
            
            var result = await _userManager.CreateAsync(user, model.Password);

            if (_userManager.Users.Count() == 1)
                await _userManager.AddToRoleAsync(user, "Admin");
            
            await _userManager.AddToRoleAsync(user, "User");

            await _cartService.AddToUser(user.Id);

            if (result.Succeeded) return Ok();
            
            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginUserRequestModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) 
                return Unauthorized();

            var passwordValid = _userManager.CheckPasswordAsync(user, model.Password);
            if (passwordValid == null) 
                return Unauthorized();

            var roles = await this._userManager.GetRolesAsync(user);

            return _identityService.GenerateJwtToken(user.Id, user.UserName, _appSettings.Secret, roles);
        }
    }
}
