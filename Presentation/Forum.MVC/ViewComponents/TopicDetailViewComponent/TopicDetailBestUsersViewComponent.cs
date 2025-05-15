using Forum.Application.Dtos.CommentDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace Forum.MVC.ViewComponents.TopicDetailViewComponent
{
    public class TopicDetailBestUsersViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        public TopicDetailBestUsersViewComponent(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("api");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _httpClient.GetAsync("Users/GetBestUsersTakeThree");
            var json = await response.Content.ReadAsStringAsync();
            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<List<BestUsersTakeThreeDto>>>(json);
            return View(posts.Data);
        }
    }
}
