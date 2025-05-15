using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<List<Post>> GetTwoPostsByCategoryIdAsync(int categoryId);
    }
}
