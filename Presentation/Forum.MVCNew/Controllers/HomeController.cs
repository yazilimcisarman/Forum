using Forum.Application.Interfaces.Services;
using Forum.MVCNew.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Forum.MVCNew.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostServices _postService;

        public HomeController(ILogger<HomeController> logger, IPostServices postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPosts();
            return View(posts.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
