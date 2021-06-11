using Microsoft.AspNetCore.Mvc;

namespace OnLineBookStore.Server.Features
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Get()
        {
            return Ok("Works");
        }

    }
}
