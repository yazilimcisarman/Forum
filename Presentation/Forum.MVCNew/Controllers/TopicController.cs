using AspNetCoreGeneratedDocument;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.MVCNew.Controllers
{
    public class TopicController : Controller
    {
        private readonly IPostServices _postServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostViewService _postViewService;

        public TopicController(IPostServices postServices, IHttpContextAccessor httpContextAccessor, IPostViewService postViewService)
        {
            _postServices = postServices;
            _httpContextAccessor = httpContextAccessor;
            _postViewService = postViewService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int topicsId)
        {
            var userIdentifier = GetUserIdentifier();
            
            var viewResponse = await _postViewService.AddViewAsync(topicsId, userIdentifier);
            var result = await _postServices.GetPostById(topicsId);
            return View(result.Data);
        }
        private string GetUserIdentifier()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
            {
                return userIdClaim.Value;
            }

            // IP adresini al
            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

            return ipAddress ?? "Unknown";
        }
    }
}
