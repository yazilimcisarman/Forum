using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Authorize]
    [Route("api/profile")]
    [ApiController]
    public class ProfilesController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostServices _postServices;
        public ProfilesController(IHttpContextAccessor httpContextAccessor, IPostServices postServices) : base(httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _postServices = postServices;
        }
        [HttpGet("getUserId")]
        public IActionResult GetUserId()
        {
            var userId = CurrentUserId;
            return Ok(new { status = true, Message = "User ID retrieved successfully.", UserId = userId });
        }
        [HttpGet]
        public IActionResult GetProfile(int? test)
        {
            //if(test != 0)
            //{
                return Ok(new { status = true, Message = "Profile retrieved successfully." });

            //}
            //return Unauthorized();
        }
        [HttpGet("getdeneme")]
        public IActionResult GetDeneme()
        {
            return Ok(new { status = true, Message = "deneme Profile retrieved successfully." });
        }
        [HttpGet("posts")]
        public async Task<IActionResult> GetUserPosts()
        {
            var userId = CurrentUserId;
            if(userId ==null)
                return Unauthorized();
            var result = await _postServices.GetUserPosts(userId);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("comments")]
        public async Task<IActionResult> GetUserComments()
        {
            var userId = CurrentUserId;
            if (userId == null)
                return Unauthorized();
            var result = await _postServices.GetUserPosts(userId);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
