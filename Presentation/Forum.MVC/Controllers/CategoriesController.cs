using Microsoft.AspNetCore.Mvc;

namespace Forum.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
