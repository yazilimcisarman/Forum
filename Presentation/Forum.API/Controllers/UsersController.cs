using Forum.Application.Dtos.UserDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userServices.GetAllUsers();
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Get User by Id
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userServices.GetByIdUser(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Create User
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user)
        {
            var result = await _userServices.CreateUser(user);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Update User
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto user)
        {
            var result = await _userServices.UpdateUser(user);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Delete User
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userServices.DeleteUser(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetBestUsersTakeThree")]
        public async Task<IActionResult> GetBestUsersTakeThree()
        {
            var result = await _userServices.GetBestUsersTakeThree();
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
