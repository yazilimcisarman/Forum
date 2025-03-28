using Forum.Application.Dtos.CommentDtos;
using Forum.Application.Dtos.SubCommentDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCommentsController : ControllerBase
    {
        private readonly ISubCommentServices _SubCommentServices;

        public SubCommentsController(ISubCommentServices SubCommentServices)
        {
            _SubCommentServices = SubCommentServices;
        }
        [HttpGet("GetAllSubComments")]
        public async Task<IActionResult> GetAllSubComments()
        {
            var result = await _SubCommentServices.GetAllSubComments();
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetByIdSubComment")]
        public async Task<IActionResult> GetByIdSubComment(int id)
        {
            var result = await _SubCommentServices.GetByIdSubComment(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("CreateSubComment")]
        public async Task<IActionResult> CreateSubComment(CreateSubCommentDto dto)
        {
            var result = await _SubCommentServices.CreateSubComment(dto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("UpdateSubComment")]
        public async Task<IActionResult> UpdateSubComment(UpdateSubCommentDto dto)
        {
            var result = await _SubCommentServices.UpdateSubComment(dto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteSubComment")]
        public async Task<IActionResult> DeleteSubComment(int id)
        {
            var result = await _SubCommentServices.DeleteSubComment(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
