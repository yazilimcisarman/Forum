using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Interfaces.Repositories;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _services;

        public AuthController(IAuthServices services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult> GenerateToken(LoginDto dto)
        {
            var result = await _services.CheckUser(dto);
            if (result.Status)
            {
                var user = await _services.GenerateToken(dto);
                return Ok(user);
            }
            return BadRequest(result);
        }
        //[Authorize]
        //[HttpPost("createRole")]
        //public async Task<IActionResult> CreateRole(string roleName)
        //{
        //    var result = await _services.CreateRole(roleName);
        //    if (result.Status)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}
        [HttpPost("addRoleToUser")]
        public async Task<IActionResult> CreateRole(string email)
        {
            var result = await _services.AddRoleToUser(email);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
