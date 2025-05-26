using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
