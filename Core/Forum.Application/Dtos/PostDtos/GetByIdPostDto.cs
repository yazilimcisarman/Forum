using Forum.Application.Dtos.CommentDtos;
using Forum.Domain.Entities;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.PostDtos
{
    //burada getbyid yerine postdetaildto olarak olsutur, dtolarda fiil eylemi olmasin get vb
    public class GetByIdPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } // Başlık
        public string Content { get; set; } // İçerik
        public DateTime CreatedAt { get; set; }
        public string CreatedAtHumanize => CreatedAt.Humanize();
        public int UserId { get; set; }
        public int CategoryId { get; set; } // Kategoriye bağlı
        public int StatusId { get; set; } // Post durumu (Gönderildi, Hazır, Yayınlandı vb.)
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Category Category { get; set; }
        public PostStatus Status { get; set; }
        public List<PostViewCommentDto> Comments { get; set; }

        public int LikeCount { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount => Comments?.Count ?? 0;
    }
}
