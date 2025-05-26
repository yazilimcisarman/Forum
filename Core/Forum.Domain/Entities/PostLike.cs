using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Domain.Entities
{
    public class PostLike
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.UtcNow;

        public Post Post { get; set; }
        public User User { get; set; }
    }
}
