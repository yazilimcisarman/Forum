using Forum.Application.Dtos.PostDtos;
using Forum.Application.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.MVC.Controllers
{
    //[Authorize]
    public class ProfilesController : Controller
    {
        private readonly HttpClient _httpClient;
        public ProfilesController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("api");
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Profiles/GetProfile");
            var json = await response.Content.ReadAsStringAsync();
            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<string>>(json);
            if(posts != null)
            {
                if (posts.Status)
                {
                    return View();
                }
            }
            return RedirectToAction("Login","Accounts");
        }
    }
}
