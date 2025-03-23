using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
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
            var result = await _categoryServices.GetAllCategoriesAsync();
            return Ok(result);  
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            await _categoryServices.CreateCategoryAsync(dto);
            return Ok("Kategori Olusturuldu.");
        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            await _categoryServices.UpdateCategoryAsync(dto);
            return Ok("Kategori Guncellendi");
        }
        [HttpGet("GetByIdCategory")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var result = await _categoryServices.GetCategoryByIdAsync(id);
            return Ok(result);
        }
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryServices.DeleteCategoryAsync(id);
            return Ok("Kategori silindi.");
        }
    }
}
