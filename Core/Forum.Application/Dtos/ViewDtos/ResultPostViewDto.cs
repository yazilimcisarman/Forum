using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.ViewDtos
{
    public class ResultPostViewDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int? UserId { get; set; }
        public string VisitorIdentifier { get; set; }
        public DateTime ViewedAt { get; set; }
    }
}
