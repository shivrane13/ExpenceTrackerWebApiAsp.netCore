using ExpenceTrackerWebApiAsp.netCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenceTrackerWebApiAsp.netCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel userData) {
            var user = new ApplicationUser { UserName = userData.UserName,Email = userData.Email };
            var result = await _userManager.CreateAsync(user, userData.Password);
            if (result.Succeeded)
            {
                return Ok("User registration successfully");
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel userData)
        {
            var user = await _userManager.FindByEmailAsync(userData.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }
            var result = await _signInManager.PasswordSignInAsync(user, userData.Password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok("Logged in successfully");
            }
            return Unauthorized("Invalid email or password");
        }
        [HttpGet("getLogedInUser")]
        public async Task<IActionResult> getLogedInUser()
        {
            var userId = _userManager.GetUserId(User);
            if(userId == null)
            {
                return Unauthorized("User is not logged in");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return NotFound("User Not Found");
            }
            var data = new RegisterModel
            {
                UserName = user.UserName,
                Email = user.Email
            };
            return Ok(data);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("User logged out successfully");
        }
    }
}
