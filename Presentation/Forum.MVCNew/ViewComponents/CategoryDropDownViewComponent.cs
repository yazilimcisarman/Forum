using Forum.Application.Interfaces.Services;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _categoryServices.GetAllCategories();
            return View(result.Data);
        }
    }
}
