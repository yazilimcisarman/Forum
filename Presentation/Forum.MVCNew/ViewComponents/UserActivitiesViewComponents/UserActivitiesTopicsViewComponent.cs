using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.ViewComponents.UserActivitiesViewComponents
{
    public class UserActivitiesTopicsViewComponent : ViewComponent
    {
        private readonly IPostServices
            _postServices;

        public UserActivitiesTopicsViewComponent(IPostServices postServices)
        {
            _postServices = postServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var result = await _postServices.GetUserPosts(userId);
            return View(result);

        }
    }
}
