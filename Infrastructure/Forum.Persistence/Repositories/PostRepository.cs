using Forum.Application.Interfaces.Repositories;
using Forum.Domain.Entities;
using Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ForumDbContext context) : base(context)
        {
        }

        public async Task<List<Post>> GetPostByCategoryId(int categoryId)
        {
            var result = await _dbSet.Where(x => x.CategoryId == categoryId).OrderDescending().ToListAsync();
            return result;
        }

        //public Task<List<Post>> GetPostsByUserIdAsync(string userId)
        //{
        //   var result = _dbSet.Include(x => x.User)
        //        .Where(x => x.User.UserIdentityId == userId)
        //        .ToListAsync();
        //    return result;  
        //}

        public async Task<List<Post>> GetTwoPostsByCategoryIdAsync(int categoryId)
        {
            //var result = await _context.Posts.Where(x => x.CategoryId == categoryId)
            var result = await _dbSet
                .Take(2)
                .ToListAsync();
            return result;
        }

        public async Task<List<Post>> GetUserPosts(string userId)
        {
            var result = await  _dbSet.Include(x => x.User)
                .Where(x => x.User.UserIdentityId == userId)
                .ToListAsync();
            return result;
        }
    }
}
