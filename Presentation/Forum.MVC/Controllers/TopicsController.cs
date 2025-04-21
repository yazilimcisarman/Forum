using Forum.Application.Dtos.PostDtos;
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

        public IActionResult Index()
        {
            var response = _httpClient.GetAsync("api/Posts/GetAllPosts").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultPostDto>>(json);
            return View(posts);
        }
        public IActionResult Detail(int? id)
        {
            return View();
        }
    }
}
