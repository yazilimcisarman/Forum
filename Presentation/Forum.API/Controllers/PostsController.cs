using Forum.Application.Dtos.PostDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostServices _postServices;
        private readonly ICategoryServices _categoryServices;

        public PostsController(IPostServices postServices, ICategoryServices categoryServices)
        {
            _postServices = postServices;
            _categoryServices = categoryServices;
        }
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(CreatePostDto dto)
        {
            var result = await _postServices.CreatePost(dto);
            if (result.Status)
            {
                var resultCategory = await _categoryServices.IncreasePostCount(dto.CategoryId);
                if (resultCategory.Status)
                {
                    return Ok(result);
                }
                return BadRequest(resultCategory);
            }
            return BadRequest(result);
        }
        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPost()
        {
            var result = await _postServices.GetAllPosts();
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var result = await _postServices.GetPostById(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost(UpdatePostDto dto)
        {
            var result = await _postServices.UpdatePost(dto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var result = await _postServices.DeletePost(postId);
            if (result.Status) { return Ok(result); }
            return BadRequest(result);
        }
        [HttpGet("GetHomePagePosts")]
        public async Task<IActionResult> GetHomePagePosts(int count=6)
        {
            var result = await _postServices.GetCountPosts(count);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
