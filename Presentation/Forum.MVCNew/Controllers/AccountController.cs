using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendLogin(LoginDto dto)
        {
            var result = await _accountServices.Login(dto);
            if (result.Status)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
