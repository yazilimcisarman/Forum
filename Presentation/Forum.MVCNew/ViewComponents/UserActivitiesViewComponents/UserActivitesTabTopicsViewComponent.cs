using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.ViewComponents.UserActivitiesViewComponents
{
    public class UserActivitesTabTopicsViewComponent : BaseViewComponent
    {
        private readonly IPostServices _postServices;
        private readonly IAccountServices _accountServices;

        public UserActivitesTabTopicsViewComponent(IPostServices postServices, IAccountServices accountServices)
        {
            _postServices = postServices;
            _accountServices = accountServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _accountServices.GetUserByUserName(GetCurrentUserName());
            var posts = await _postServices.GetUserPosts(user.Data.Id);
            return View(posts.Data);
        }
    }
}
