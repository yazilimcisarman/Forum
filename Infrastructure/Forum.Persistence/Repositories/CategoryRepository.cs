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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ForumDbContext _context;
        public CategoryRepository(ForumDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetHomePopularCategory()
        {
            return await _context.Categories.OrderByDescending(c => c.PostCount).Take(4).ToListAsync();
        }
    }
}
