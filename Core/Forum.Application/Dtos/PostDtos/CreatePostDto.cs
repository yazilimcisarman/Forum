using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.PostDtos
{
    public class CreatePostDto
    {
        public string Title { get; set; } // Başlık
        public string Content { get; set; } // İçerik
        public int UserId { get; set; }
        //public string Username { get; set; } // Kullanıcı adı ile eşleşecek
        public int CategoryId { get; set; } // Kategoriye bağlı
    }
}
