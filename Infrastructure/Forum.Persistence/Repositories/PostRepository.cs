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
    public class PostRepository : IPostRepository
    {
        private readonly ForumDbContext _context;

        public PostRepository(ForumDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetTwoPostsByCategoryIdAsync(int categoryId)
        {
            //var result = await _context.Posts.Where(x => x.CategoryId == categoryId)
            var result = await _context.Posts
                .Take(2)
                .ToListAsync();
            return result;
        }
    }
}
