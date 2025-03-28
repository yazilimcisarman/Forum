using Forum.Application.Dtos.CommentDtos;
using Forum.Application.Dtos.SubCommentDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentServices _commentServices;

        public CommentsController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetAllComments() 
        {
            var result = await _commentServices.GetAllComments();
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetByIdComment")]
        public async Task<IActionResult> GetByIdComment(int id)
        {
            var result = await _commentServices.GetByIdComment(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment(CreateCommentDto dto)
        {
            var result = await _commentServices.CreateComment(dto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto dto)
        {
            var result = await _commentServices.UpdateComment(dto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentServices.DeleteComment(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
