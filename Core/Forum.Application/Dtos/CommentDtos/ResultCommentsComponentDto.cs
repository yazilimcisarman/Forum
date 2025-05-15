using Forum.Application.Dtos.SubCommentDtos;
using Forum.Domain.Entities;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.CommentDtos
{
    public class ResultCommentsComponentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } // Yorum içeriği
        public DateTime CreatedAt { get; set; }
        public string CreatedAtHumanize => CreatedAt.Humanize();
        public int UserId { get; set; }
        public int PostId { get; set; } 
        public User User { get; set; }
        public List<ResultSubCommentDto> SubComments { get; set; }
    }
}
