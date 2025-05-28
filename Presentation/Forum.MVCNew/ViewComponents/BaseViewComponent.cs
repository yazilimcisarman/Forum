using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.ViewComponents
{
    public abstract class BaseViewComponent : ViewComponent
    {
        protected string GetCurrentUserName()
        {
            return User.Identity?.Name ?? "Guest";
        }
    }
}
