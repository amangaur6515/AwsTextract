using AwsTextract.api.Models;
using AwsTextract.api.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AwsTextract.api.Services.Implementation
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }
        public async  Task<UserManagerResponseViewModel> RegisterUserAsync(User obj)
        {
            var user = new IdentityUser() { UserName = obj.Email, Email = obj.Email };
            var result = await _userManager.CreateAsync(user, obj.Password); //bool result, identity result
                                                                             //if user created successfully
            if (result.Succeeded)
            {
                //we can send a welcome email
                return new UserManagerResponseViewModel
                {
                    Message = "User created successfully",
                    IsSuccess = true,
                    
                };
            }

            return new UserManagerResponseViewModel
            {
                Message = "User couldn't be created",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };


        }

        public async Task<UserManagerResponseViewModel> LoginUserAsync(User userObj)
        {
            var result = await _signInManager.PasswordSignInAsync(userObj.Email, userObj.Password, false, false);
            if (result.Succeeded)
            {
                //generate token
                string token=GenerateToken(userObj);
                return new UserManagerResponseViewModel
                {
                    Message = token,
                    IsSuccess = true,
                   
                };

            }
            return new UserManagerResponseViewModel
            {
                Message = "Invalid credential",
                IsSuccess = false,

            };
        }

        private string GenerateToken(User userObj)
        {
            //create claims
            var claims = new[]
            {
                new Claim("Email",userObj.Email),
            };

            //get the key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString=new JwtSecurityTokenHandler().WriteToken(token);
            return tokenAsString;
        }
    }
}
