using Forum.Application.Dtos.ProfileDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.MVCNew.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IPostServices _postServices;

        public ProfileController(IPostServices postServices)
        {
            _postServices = postServices;
        }

        public IActionResult Index()
        {
            var user = User.Identity.Name;
            var result = new ProfileNameDto
            {
                Name = user
            };
            return View(result);
        }
        public async Task<IActionResult> MyPosts()
        {
            var userId = User.FindFirstValue("UserIdentityId");
            var posts = await _postServices.GetUserPosts(userId);
            return View(posts);
        }
    }
}
