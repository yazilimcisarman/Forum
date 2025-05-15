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

            var response = await _httpClient.PostAsync("Accounts/Login", jsonContent);
            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ApiResponse<string>>(json); // string yerine token dönebilir

            if (result.Status)
            {
                // Token'ı saklamak ya da işlem yapmak
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
