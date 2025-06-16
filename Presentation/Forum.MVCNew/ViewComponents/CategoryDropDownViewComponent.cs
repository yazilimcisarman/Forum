using Forum.Application.Interfaces.Services;
using Forum.MVCNew.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forum.MVCNew.ViewComponents
{
    public class CategoryDropDownViewComponent : ViewComponent
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryDropDownViewComponent(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedCategoryId)
        {
            var result = await _categoryServices.GetAllCategories();
            var value = new CategoryDropDownViewModel
            {
                Categories = result.Data,
                SelectedCategoryId = selectedCategoryId
            };
            return View(value);
        }
    }
}
