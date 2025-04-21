using Forum.Application.Dtos.PostDtos;
using Forum.Application.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Mvc;

namespace Forum.MVC.Controllers
{
    public class TopicsController : Controller
    {
        private readonly HttpClient _httpClient;

        public TopicsController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Posts/GetAllPosts");
            var json = await response.Content.ReadAsStringAsync();
            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<List<ResultPostDto>>>(json);
            return View(posts.Data);
        }
        public IActionResult Detail(int? id)
        {
            return View();
        }
    }
}
