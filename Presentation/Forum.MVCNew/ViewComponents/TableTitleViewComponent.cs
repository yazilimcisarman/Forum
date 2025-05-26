using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.ViewComponents
{
    public class TableTitleViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
           
            return View();
        }
    }
}
