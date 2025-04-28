using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
                var result = await _categoryServices.GetAllCategories();
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            var result = await _categoryServices.CreateCategory(dto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            var result = await _categoryServices.UpdateCategory(dto);
            if(result.Status)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("GetByIdCategory")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var result = await _categoryServices.GetCategoryById(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryServices.DeleteCategory(id);
            if (result.Status)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("GetHomePopularCategory")]
        public async Task<IActionResult> GetHomePopularCategory()
        {
            var result = await _categoryServices.GetHomePopularCategory();
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
