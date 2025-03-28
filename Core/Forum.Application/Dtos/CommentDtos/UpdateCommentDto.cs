using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.CommentDtos
{
    public class UpdateCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } // Yorum içeriği
    }
}
