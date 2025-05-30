using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{code:int}")]
        public IActionResult HttpStatusCodeHandler(int code)
        {
            if (code == 404)
            {
                return View("NotFound");
            }

            return View("GenericError");
        }

        [Route("Error/500")]
        public IActionResult ServerError()
        {
            return View("ServerError");
        }
    }
}
