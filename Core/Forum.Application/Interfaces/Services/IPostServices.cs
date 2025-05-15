using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Dtos.PostDtos;
using Forum.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Services
{
    public interface IPostServices
    {
        Task<ApiResponse<List<ResultPostDto>>> GetAllPosts(); 
        Task<ApiResponse<GetByIdPostDto>> GetPostById(int PostId); 
        Task<ApiResponse<object>> CreatePost(CreatePostDto post); 
        Task<ApiResponse<object>> UpdatePost(UpdatePostDto post); 
        Task<ApiResponse<object>> DeletePost(int postId);
        Task<ApiResponse<List<ResultPostDto>>> GetCountPosts(int count);
        //Task<ApiResponse<List<ResultPostDto>>> GetAllPostsInclude();
        Task<ApiResponse<List<DetailPostByCategoryDto>>> GetTwoPostsByCategoryId(int categoryId);
    }
}
