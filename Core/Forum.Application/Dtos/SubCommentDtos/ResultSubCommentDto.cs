using Forum.Domain.Entities;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.SubCommentDtos
{
    public class ResultSubCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } // Yorum içeriği
        public DateTime CreatedAt { get; set; }
        public string CreatedAtHumanize => CreatedAt.Humanize();
        public int? UserId { get; set; }
        public string Username { get; set; } // Kullanıcı adı
        public int CommentId { get; set; } // Bağlı olduğu ana yorum

        public User? User { get; set; }
        //public Comment Comment { get; set; } // Ana yoruma bağlı olacak
    }
}
