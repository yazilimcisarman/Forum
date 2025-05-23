﻿using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.PostDtos
{
    public class UpdatePostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } // Başlık
        public string Content { get; set; } // İçerik
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public int CategoryId { get; set; } // Kategoriye bağlı
        public int StatusId { get; set; } // Post durumu (Gönderildi, Hazır, Yayınlandı vb.)
        public DateTime? ModifiedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        //public List<Comment> Comments { get; set; } //buna bakicaz 
    }
}
