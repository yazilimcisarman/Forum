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
    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly ForumDbContext _context;

        public PostLikeRepository(ForumDbContext context)
        {
            _context = context;
        }

        public async Task<PostLike> GetLikeAsync(int postId, int userId)
        {
            return await _context.PostLikes
                .FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
        }
        public async Task<bool> IsPostLikedByUserAsync(int postId, int userId)
        {
            return await _context.PostLikes.AnyAsync(x => x.PostId == postId && x.UserId == userId);
        }

        public async Task<int> GetLikeCountAsync(int postId)
        {
            return await _context.PostLikes.CountAsync(x => x.PostId == postId);
        }

        public async Task AddLikeAsync(PostLike like)
        {
            await _context.PostLikes.AddAsync(like);
        }

        public async Task RemoveLikeAsync(PostLike like)
        {
            _context.PostLikes.Remove(like);
        }
    }
}
