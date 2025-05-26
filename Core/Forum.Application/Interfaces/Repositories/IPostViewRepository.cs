using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Repositories
{
    public interface IPostViewRepository
    {
        Task<bool> IsPostViewedByUserAsync(int postId, string userId);
        Task<int> GetViewCountAsync(int postId);
        Task AddViewAsync(PostView view);
    }
}
