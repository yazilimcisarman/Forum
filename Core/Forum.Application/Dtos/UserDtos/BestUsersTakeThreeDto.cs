using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.UserDtos
{
    public class BestUsersTakeThreeDto
    {
        public int Id { get; set; }
        public string UserIdentityId { get; set; }
        public string UserName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public int PostCount { get; set; } = 12;
    }
}
