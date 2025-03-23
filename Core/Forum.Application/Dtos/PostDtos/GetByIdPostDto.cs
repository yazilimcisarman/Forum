using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.PostDtos
{
    public class GetByIdPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } // Başlık
        public string Content { get; set; } // İçerik
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public string Username { get; set; } // Kullanıcı adı ile eşleşecek
        public int CategoryId { get; set; } // Kategoriye bağlı
        public int StatusId { get; set; } // Post durumu (Gönderildi, Hazır, Yayınlandı vb.)
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Category Category { get; set; }
        public PostStatus Status { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
