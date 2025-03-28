using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.CommentDtos
{
    public class CreateCommentDto
    {
        public string Content { get; set; } // Yorum içeriği
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public int PostId { get; set; } // Bağlı olduğu post
        
    }
}
