using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Dtos.PostDtos;
using Forum.Application.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Forum.MVC.Controllers
{
    public class AccountsController : Controller
    {
        private readonly HttpClient _httpClient;
        public AccountsController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("api");
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendLogin(LoginDto dto)
        {
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(dto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("Auth", jsonContent);
            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ApiResponse<string>>(json); // string yerine token dönebilir

            if (result.Status)
            {
                //burasini servise al

                HttpContext.Response.Cookies.Append("_t", result.Data, new CookieOptions
                {
                    HttpOnly = true, // JavaScript erişemesin, güvenlik için önemli
                    Secure = true,   // HTTPS zorunluysa true olmalı
                    SameSite = SameSiteMode.Strict, // CSRF için koruma
                    Expires = DateTimeOffset.Now.AddDays(1) // Token süresi kadar geçerli
                });

                //#####################
                return RedirectToAction("Index", "Profiles");
            }

            ModelState.AddModelError("", result.ErrorMessage ?? "Giriş başarısız.");
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
