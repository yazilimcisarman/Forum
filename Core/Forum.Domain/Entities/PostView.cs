using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Domain.Entities
{
    public class PostView
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string? UserId { get; set; } // Anonimse null
        public string VisitorIdentifier { get; set; } // IP veya cookie hash
        public DateTime ViewedAt { get; set; }

        public Post Post { get; set; }
    }
}
