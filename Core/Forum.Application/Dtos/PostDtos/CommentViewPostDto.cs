using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Dtos.PostStatusDtos;
using Forum.Application.Dtos.UserDtos;
using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.PostDtos
{
    public class CommentViewPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } // Başlık
        public string Content { get; set; } // İçerik
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public int CategoryId { get; set; } // Kategoriye bağlı
        public int StatusId { get; set; } = 1; // Post durumu (Gönderildi, Hazır, Yayınlandı vb.)
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation Properties
        public GetByIdUserDto User { get; set; }
        public GetByIdCategoryDto Category { get; set; }
        public GetByIdPostStatusDto Status { get; set; }
    }
}
