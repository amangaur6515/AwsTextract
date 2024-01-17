using AwsTextract.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwsTextract.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
            
        }

        

        [HttpGet("Test")]
        public IActionResult Test()
        {
            //var userid = User.Identity.IsAuthenticated
            //return Ok(new { Message = userid});
            return Ok(new { Message = "Hello world" });
           
        }
    }
}
