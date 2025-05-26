using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetTwoPostsByCategoryIdAsync(int categoryId);
        Task<List<Post>> GetUserPosts(string userId);
        Task<List<Post>> GetPostByCategoryId(int categoryId);
        //Task<List<Post>> GetPostsByUserIdAsync(string userId);
    }
}
