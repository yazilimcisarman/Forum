using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Dtos.CommentDtos;
using Forum.Application.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Mvc;

namespace Forum.MVC.ViewComponents.TopicDetailViewComponent
{
    public class TopicDetailCommentsViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        public TopicDetailCommentsViewComponent(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("api");
        }
        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            var response = await _httpClient.GetAsync("Comments/GetCommentsByPostId?postId="+ postId);
            var json = await response.Content.ReadAsStringAsync();
            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<List<ResultCommentsComponentDto>>>(json);
            return View(posts.Data);
        }
    }
}
