using Forum.Application.Dtos.CommentDtos;
using Forum.Application.Dtos.PostDtos;
using Forum.Application.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Mvc;

namespace Forum.MVC.ViewComponents.TopicDetailViewComponent
{
    public class TopicDetailSampleTopicsViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        public TopicDetailSampleTopicsViewComponent(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("api");
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var response = await _httpClient.GetAsync("Posts/GetSampleTopicsTwoCount?categoryId=" + categoryId);
            var json = await response.Content.ReadAsStringAsync();
            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<List<DetailPostByCategoryDto>>>(json);
            return View(posts.Data);
        }
    }
}
