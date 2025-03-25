using Forum.Application.Dtos.UserDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        // Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userServices.GetAllUsers();
            return Ok(users);
        }

        // Get User by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userServices.GetByIdUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Create User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            await _userServices.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Username }, user);
        }

        // Update User
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto user)
        {
            if (user == null || id != user.Id)
            {
                return BadRequest();
            }

            await _userServices.UpdateUser(user);
            return NoContent();
        }

        // Delete User
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userServices.GetByIdUser(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userServices.DeleteUser(id);
            return NoContent();
        }
    }
}
