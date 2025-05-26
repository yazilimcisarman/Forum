using Forum.Application.Dtos.CommentDtos;
using Forum.Application.Dtos.PostDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Services
{
    public interface ICommentServices
    {
        Task<ApiResponse<GetByIdCommentDto>> GetByIdComment(int id);
        Task<ApiResponse<List<ResultCommentDto>>> GetAllComments();
        Task<ApiResponse<object>> CreateComment(CreateCommentDto Comment);
        Task<ApiResponse<object>> UpdateComment(UpdateCommentDto Comment);
        Task<ApiResponse<object>> DeleteComment(int id);
        Task<ApiResponse<List<ResultCommentsComponentDto>>> GetAllCommentsByPostId(int postId);
        Task<ApiResponse<List<ResultCommentDto>>> GetUserComments(string userId);
    }
}
