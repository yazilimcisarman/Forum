using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Dtos.PostDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;

namespace Forum.MVC.ViewComponents.HomeViewComponent
{
    public class HomePopularCategoryViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public HomePopularCategoryViewComponent(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("api");
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _httpClient.GetAsync("Categories/GetHomePopularCategory");
            var json = await response.Content.ReadAsStringAsync();
            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<List<ResultCategoryDto>>>(json);
            return View(posts.Data);
        }
    }
}
