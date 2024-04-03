using JellBreak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using JellBreak.Models;
using JellBreak.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using MongoDB.Driver;

namespace JellBreak.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public UserController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest("An account with this email address already exists.");
            }

            var user = new UserModel
            {
                Email = model.Email,
                Username = model.Username,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok("Account created successfully.");
            }

            return BadRequest(result.Errors.First().Description);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                return Ok("Login successful.");
            }

            return BadRequest("Invalid email or password.");
        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult GetUser()
        {
            var user = _userManager.GetUserAsync(User).Result;

            if (user != null)
            {
                return Ok(new { Email = user.Email, Username = user.Username });
            }

            return NotFound();
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok("Logout successful.");
        }
    }

}
