using Microsoft.AspNetCore.Mvc;

namespace Forum.MVC.ViewComponents
{
    public class HomePagePopularCategoriesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
