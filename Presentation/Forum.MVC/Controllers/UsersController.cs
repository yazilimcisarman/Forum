using Microsoft.AspNetCore.Mvc;

namespace Forum.MVC.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index(int? id)
        {
            return View();
        }
    }
}
