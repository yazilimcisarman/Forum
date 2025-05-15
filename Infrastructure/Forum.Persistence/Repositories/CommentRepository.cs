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
    public class CommentRepository : ICommentRepository
    {
        private readonly ForumDbContext _context;

        public CommentRepository(ForumDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllCommentsByPostId(int postId)
        {
            var result = await _context.Comments.Where(c => c.PostId == postId).Include(c => c.User).Include(x => x.SubComments).ThenInclude(x => x.User)
                //.AsNoTracking()
                .ToListAsync();
            return result;
        }
    }
}
