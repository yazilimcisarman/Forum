using Forum.Application.Dtos.PostLikeDtos;
using Forum.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.MVCNew.Controllers
{
    public class LikeController : Controller
    {
        private readonly PostLikeService _likeService;

        public LikeController(PostLikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        public async Task<JsonResult> AddLike(CreatePostLikeDto dto)
        {
            var response = await _likeService.AddLikeAsync(dto);
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> RemoveLike(DeletePostLikeDto dto)
        {
            var response = await _likeService.RemoveLikeAsync(dto);
            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> IsLiked(int postId, int userId)
        {
            var response = await _likeService.IsLikedAsync(postId, userId);
            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> LikeCount(int postId)
        {
            var response = await _likeService.LikeCountAsync(postId);
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> ToggleLike(CreatePostLikeDto dto)
        {
            var response = await _likeService.ToggleLikeAsync(dto);
            return Json(response);
        }
    }
}
