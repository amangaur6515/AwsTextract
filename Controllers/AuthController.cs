using AwsTextract.api.Models;
using AwsTextract.api.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AwsTextract.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthService _authService;
        public AuthController(SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager,IAuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User obj)
        {
            if (ModelState.IsValid)
            {
                UserManagerResponseViewModel res = await _authService.RegisterUserAsync(obj);
                return Ok(res);
            }
            ModelState.AddModelError("", "invalid");
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn([FromBody] User userObj)
        {
            if (ModelState.IsValid)
            {
                var res=await _authService.LoginUserAsync(userObj);
                return Ok(res);

            }
            ModelState.AddModelError("", "Invalid Credentials");
            return BadRequest(ModelState);


        }


    }
}
