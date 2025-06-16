using AspNetCoreGeneratedDocument;
using Forum.Application.Dtos.PostDtos;
using Forum.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.MVCNew.Controllers
{
    public class TopicController : Controller
    {
        private readonly IPostServices _postServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostViewService _postViewService;
        private readonly IAccountServices _accountServices;
        private readonly IUserServices _userServices;

        public TopicController(IPostServices postServices, IHttpContextAccessor httpContextAccessor, IPostViewService postViewService, IAccountServices accountServices, IUserServices userServices)
        {
            _postServices = postServices;
            _httpContextAccessor = httpContextAccessor;
            _postViewService = postViewService;
            _accountServices = accountServices;
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDto dto)
        {
            var user = await _accountServices.GetUserByUserName(User.Identity.Name);
            var userId = await _userServices.GetByUserIdentityId(user.Data.Id);
            dto.UserId = userId.Data.Id;
            var result = await _postServices.CreatePost(dto);
            if (!result.Status)
            {
                return View(result.ErrorMessage);
            }
            return RedirectToAction("Index","Home");
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

        [Authorize]
        public async Task<IActionResult> Edit(int topicId)
        {
            var user = await _accountServices.GetUserByUserName(User.Identity.Name);
            var userId = await _userServices.GetByUserIdentityId(user.Data.Id);

            var result = await _postServices.GetPostById(topicId);
            if (result.Data.User.UserIdentityId != userId.Data.UserIdentityId)
            {
                return RedirectToAction("404","Error");
            }
            return View(result.Data);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdatePost(UpdatePostDto dto)
        {
            //en son burada kullanici id y ialamiyorduk onu ayarla
            //var userIdentifier = GetUserIdentifier();
            var user = await _accountServices.GetUserByUserName(User.Identity.Name);
            var userId = await _userServices.GetByUserIdentityId(user.Data.Id);
            //dto.UserId = userIdentifier; // Kullanıcı kimliğini ayarla
            dto.UserId = userId.Data.Id;
            var result = await _postServices.UpdatePost(dto);
            if (result.Status)
            {
                return RedirectToAction("Index","Profile");
            }
            return View(dto);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _accountServices.GetUserByUserName(User.Identity.Name);
            var userId = await _userServices.GetByUserIdentityId(user.Data.Id);

            var result = await _postServices.GetPostById(id);
            if (result.Data.User.UserIdentityId != userId.Data.UserIdentityId)
            {
                return RedirectToAction("404", "Error");
            }
            var resultDelete = await _postServices.DeletePost(id);
            if (resultDelete.Status)
            {

                return RedirectToAction("Index", "Profile");
                }
            return RedirectToAction("500", "Error");

        }
    }
}
