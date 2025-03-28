using Forum.Application.Dtos.PostDtos;
using Forum.Application.Dtos.SubCommentDtos;
using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.CommentDtos
{
    public class PostViewCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } // Yorum içeriği
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public int PostId { get; set; } // Bağlı olduğu post

        // Navigation Properties
        public User User { get; set; }
        public List<ResultSubCommentDto> SubComments { get; set; }
    }
}
