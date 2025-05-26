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
    public class PostViewRepository : IPostViewRepository
    {
        private readonly ForumDbContext _context;

        public PostViewRepository(ForumDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsPostViewedByUserAsync(int postId, string userId)
        {
            return await _context.PostViews.AnyAsync(x => x.PostId == postId && x.UserId == userId);
        }

        public async Task<int> GetViewCountAsync(int postId)
        {
            return await _context.PostViews.CountAsync(x => x.PostId == postId);
        }

        public async Task AddViewAsync(PostView view)
        {
            await _context.PostViews.AddAsync(view);
            await _context.SaveChangesAsync();
        }
    }
}
