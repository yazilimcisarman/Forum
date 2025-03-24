using Forum.Application.Dtos.PostStatusDtos;
using Forum.Application.Interfaces.Services;
using Forum.Application.Services;
using Microsoft.AspNetCore.Http;
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
            var result = await _postStatusServices.GetAllPostStatusAsync();
            return Ok(result);  
        }
        [HttpGet("getPostStatusById/{id}")]
        public async Task<IActionResult> GetPostStatusById(int id)
        {
            var result = await _postStatusServices.GetPostStatusByIdAsync(id);
            if (result == null)
                return NotFound("PostStatus bulunamadı.");

            return Ok(result);
        }

        [HttpPost("createPostStatus")]
        public async Task<IActionResult> CreatePostStatus([FromBody] CreatePostStatusDto postStatusDto)
        {
            if (postStatusDto == null || string.IsNullOrWhiteSpace(postStatusDto.Name))
                return BadRequest("Geçersiz veri.");

            await _postStatusServices.CreatePostStatusAsync(postStatusDto);
            return Ok("PostStatus başarıyla oluşturuldu.");
        }

        [HttpPut("updatePostStatus")]
        public async Task<IActionResult> UpdatePostStatus([FromBody] UpdatePostStatusDto postStatusDto)
        {
            if (postStatusDto == null || postStatusDto.Id <= 0 || string.IsNullOrWhiteSpace(postStatusDto.Name))
                return BadRequest("Geçersiz veri.");

            await _postStatusServices.UpdatePostStatusAsync(postStatusDto);
            return Ok("PostStatus başarıyla güncellendi.");
        }

        [HttpDelete("deletePostStatus/{id}")]
        public async Task<IActionResult> DeletePostStatus(int id)
        {
            await _postStatusServices.DeletePostStatusAsync(id);
            return Ok("PostStatus başarıyla silindi.");
        }
    }
}
