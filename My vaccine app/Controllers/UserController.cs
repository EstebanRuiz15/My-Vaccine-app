using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Repositories.Implementations;
using My_vaccine_app.Repositories.Interfaces;
using My_vaccine_app.Services.Contracts;
using System.Security.Claims;

namespace My_vaccine_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController( IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _userService.GetById(id);
            if (response == null)
                return NotFound($"User with id {id} not found");
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound($"Not found user with id: {id}");

            await _userService.Delete(id);
            return Ok($"delete successfully user: {user.FirstName} {user.LastName}");
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetInfoUser()
        {
            var claimIdentity = HttpContext.User.Identity as ClaimsIdentity;
            if (!claimIdentity.IsAuthenticated)
                return Unauthorized();
            var response = await _userService.GetUserInfo(claimIdentity.Name);
            
            return Ok(response);
        }
    }
}
