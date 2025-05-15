using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetProfile()
        {
            return Ok( new {status=true,Message = "Profile retrieved successfully." });
        }
        [HttpGet("getdeneme")]
        public IActionResult GetDeneme()
        {
            return Ok(new { status = true, Message = "deneme Profile retrieved successfully." });
        }
    }
}
