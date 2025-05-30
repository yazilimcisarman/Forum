using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Interfaces.Services;
using Forum.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            if (!ModelState.IsValid)
                return View("Login", dto); // validation hataları

            var result = await _accountServices.Login(dto);
            if (result.Status)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı!");
            return View("Login", dto);
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            var result = await _accountServices.LogOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
