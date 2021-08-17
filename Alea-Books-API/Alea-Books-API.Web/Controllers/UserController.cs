using Microsoft.AspNetCore.Mvc;

namespace Alea_Books_API.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Test");
        }
    }
}
