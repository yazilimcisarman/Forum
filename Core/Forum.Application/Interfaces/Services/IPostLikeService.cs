using Forum.Application.Dtos.PostLikeDtos;
using Forum.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Services
{
    public interface IPostLikeService
    {
        Task<ApiResponse<object>> AddLikeAsync(CreatePostLikeDto dto);
        Task<ApiResponse<object>> RemoveLikeAsync(DeletePostLikeDto dto);
        Task<ApiResponse<object>> IsLikedAsync(int postId, int userId);
        Task<ApiResponse<object>> LikeCountAsync(int postId);
        Task<ApiResponse<object>> ToggleLikeAsync(CreatePostLikeDto dto);
    }
}
