using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.UserDtos
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string UserIdentityId { get; set; } //identity icin
        public string Username { get; set; } // Kullanıcı adı (Identity ile eşleşecek)
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? ProfilePictureUrl { get; set; } // Profil resmi
        public bool CanPost { get; set; } = true; // Post atma yetkisi
        public string Email { get; set; }
    }
}
