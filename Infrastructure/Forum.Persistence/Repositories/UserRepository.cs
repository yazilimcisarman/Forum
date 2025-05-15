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
    public class UserRepository : IUserRepository
    {
        private readonly ForumDbContext _dbContext;

        public UserRepository(ForumDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetBestUsersTakeThree()
        {
            var result = await _dbContext.Users
                //.OrderByDescending(x => x.Posts.Count)
                //.ThenByDescending(x => x.Comments.Count)
                .Take(3)
                .ToListAsync();
            return result;
        }
    }
}
