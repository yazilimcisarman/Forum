using Forum.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Services
{
    public interface IPostViewService
    {
        Task<ApiResponse<object>> AddViewAsync(int postId, string userId);
        Task<ApiResponse<object>> GetViewCountAsync(int postId);
        Task<ApiResponse<object>> IsViewedAsync(int postId, string userId);
    }
}
