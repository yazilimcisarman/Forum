using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryService;

        public CategoryController(ICategoryServices categoryService)
        {
            _categoryService = categoryService;
        }

        public async  Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories();   
            return View(categories.Data);
        }
        public async Task<IActionResult> Detail(int categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);
            return View(category.Data);
        }
    }
}
