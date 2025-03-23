using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Domain.Entities
{
    public class SubComment
    {
        public int Id { get; set; }
        public string Content { get; set; } // Yorum içeriği
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? UserId { get; set; }
        public string Username { get; set; } // Kullanıcı adı
        public int CommentId { get; set; } // Bağlı olduğu ana yorum

        public User? User { get; set; }
        public Comment Comment { get; set; } // Ana yoruma bağlı olacak
    }
}
