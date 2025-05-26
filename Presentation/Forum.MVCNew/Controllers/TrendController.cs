using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.Controllers
{
    public class TrendController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
