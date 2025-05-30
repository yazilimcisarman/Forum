using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Forum.MVCNew.ViewComponents.UserActivitiesViewComponents
{
    public class UserActivitiesTabCommentsViewComponent : BaseViewComponent
    {
        private readonly IAccountServices _accountServices;
        private readonly ICommentServices _commentServices;

        public UserActivitiesTabCommentsViewComponent(IAccountServices accountServices, ICommentServices commentServices)
        {
            _accountServices = accountServices;
            _commentServices = commentServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _accountServices.GetUserByUserName(GetCurrentUserName());
            var comments = await _commentServices.GetUserComments(user.Data.Id);
            return View(comments.Data);
        }
    }
}
