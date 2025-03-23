using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } // Yorum içeriği
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? UserId { get; set; }
        public string Username { get; set; } // Kullanıcı adı
        public int PostId { get; set; } // Bağlı olduğu post

        // Navigation Properties
        public User? User { get; set; }
        public Post Post { get; set; }
        public List<SubComment> SubComments { get; set; }
    }
}
