using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Dtos.PostStatusDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Services
{
    public interface IPostStatusServices
    {
        Task<List<ResultPostStatusDto>> GetAllPostStatusAsync(); 
        Task<GetByIdPostStatusDto> GetPostStatusByIdAsync(int PostStatusId); 
        Task CreatePostStatusAsync(CreatePostStatusDto PostStatus); 
        Task UpdatePostStatusAsync(UpdatePostStatusDto PostStatus); 
        Task DeletePostStatusAsync(int PostStatusId); 
    }
}
