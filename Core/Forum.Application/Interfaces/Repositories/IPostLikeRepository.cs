using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Repositories
{
    public interface IPostLikeRepository
    {
        Task<bool> IsPostLikedByUserAsync(int postId, int userId);
        Task<int> GetLikeCountAsync(int postId);
        Task AddLikeAsync(PostLike like);
        Task RemoveLikeAsync(PostLike like);
        Task<PostLike> GetLikeAsync(int postId, int userId);
    }
}
