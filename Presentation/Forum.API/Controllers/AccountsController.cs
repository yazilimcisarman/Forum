using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountServices _accountServices;

        public AccountsController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _accountServices.Login(dto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _accountServices.CreateUser(dto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var result = await _accountServices.GetUserById(userId);
            if (result.Status) 
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _accountServices.GetUserByEmail(email);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("LogOut")]
        public async Task<IActionResult> Logout()
        {
            var result = await _accountServices.LogOut();
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetAuthUser")]
        public async Task<IActionResult> GetAuthUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _accountServices.GetUserById(userId);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
