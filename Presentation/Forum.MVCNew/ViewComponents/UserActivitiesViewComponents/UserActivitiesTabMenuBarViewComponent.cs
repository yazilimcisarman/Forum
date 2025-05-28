using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.ViewComponents.UserActivitiesViewComponents
{
    public class UserActivitiesTabMenuBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
