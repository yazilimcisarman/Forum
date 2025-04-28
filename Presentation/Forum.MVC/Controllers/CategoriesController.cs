using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Mvc;

namespace Forum.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoriesController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Categories/GetAllCategory");
            var json = await response.Content.ReadAsStringAsync();
            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<List<ResultCategoryDto>>>(json);
            return View(posts.Data);
        }
    }
}
