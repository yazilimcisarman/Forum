using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

            public IActionResult Register()
        {
            return View();
        }
    }
}
