using Forum.Application.Dtos.CommentDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Dtos.SubCommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Services
{
    public interface ISubCommentServices
    {
        Task<ApiResponse<GetByIdSubCommentDto>> GetByIdSubComment(int id);
        Task<ApiResponse<List<ResultSubCommentDto>>> GetAllSubComments();
        Task<ApiResponse<object>> CreateSubComment(CreateSubCommentDto SubComment);
        Task<ApiResponse<object>> UpdateSubComment(UpdateSubCommentDto SubComment);
        Task<ApiResponse<object>> DeleteSubComment(int id);
    }
}
