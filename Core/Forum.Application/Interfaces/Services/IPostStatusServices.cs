using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Dtos.PostStatusDtos;
using Forum.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Services
{
    public interface IPostStatusServices
    {
        Task<ApiResponse<List<ResultPostStatusDto>>> GetAllPostStatus(); 
        Task<ApiResponse<GetByIdPostStatusDto>> GetByIdPostStatus(int PostStatusId); 
        Task<ApiResponse<object>> CreatePostStatus(CreatePostStatusDto PostStatus); 
        Task<ApiResponse<object>> UpdatePostStatus(UpdatePostStatusDto PostStatus); 
        Task<ApiResponse<object>> DeletePostStatus(int PostStatusId); 
    }
}
