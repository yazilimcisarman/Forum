using Forum.Application.Dtos.PostStatusDtos;
using Forum.Application.Interfaces.Services;
using Forum.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostStatusesController : ControllerBase
    {
        private readonly IPostStatusServices _postStatusServices;

        public PostStatusesController(IPostStatusServices postStatusServices)
        {
            _postStatusServices = postStatusServices;
        }
        [HttpGet("getAllPostStatus")]
        public async Task<IActionResult> GetAllPostStatus()
        {
            var result = await _postStatusServices.GetAllPostStatus();
            if (result.Status) 
            {
                return Ok(result);
            }
            return BadRequest(result);  
        }
        [HttpGet("getPostStatusById/{id}")]
        public async Task<IActionResult> GetPostStatusById(int id)
        {
            var result = await _postStatusServices.GetByIdPostStatus(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("createPostStatus")]
        public async Task<IActionResult> CreatePostStatus([FromBody] CreatePostStatusDto postStatusDto)
        {
            var result = await _postStatusServices.CreatePostStatus(postStatusDto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("updatePostStatus")]
        public async Task<IActionResult> UpdatePostStatus([FromBody] UpdatePostStatusDto postStatusDto)
        {
            var result = await _postStatusServices.UpdatePostStatus(postStatusDto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("deletePostStatus/{id}")]
        public async Task<IActionResult> DeletePostStatus(int id)
        {
            var result= await _postStatusServices.DeletePostStatus(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
