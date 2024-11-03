using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using My_vaccine_app.Dtos;
using My_vaccine_app.Literals;
using My_vaccine_app.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace My_vaccine_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        public AuthController(UserManager<IdentityUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] RequestRegisterDto model)
        {
            
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
             if (!IsValidEmail(model.Email))
              {
              return BadRequest("the format email not is correct");
              }

            var result = await _userRepository.addUser(model);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new { message = "User registration failed", errors });
            }

            return Ok("User register successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> login([FromBody] LoginRequestDto model)
        {
            var valid = await _userRepository.login(model);

            if (valid.token != null)
            {

                return Ok(valid);

            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPost("refresh")]
        public async Task<IActionResult> refreshToken()
        {
            var clainIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var response = await _userRepository.refresh(clainIdentity.Name);

            if (response.token != null)
            {
                return Ok(response);
            }

            return Unauthorized();
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
