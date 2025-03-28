using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.SubCommentDtos
{
    public class CreateSubCommentDto
    {
        public string Content { get; set; } // Yorum içeriği
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? UserId { get; set; }
        public int CommentId { get; set; } // Bağlı olduğu ana yorum

    }
}
