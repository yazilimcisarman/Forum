using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.UserDtos
{
    public class CreateUserDto
    {
        public string UserIdentityId { get; set; } //identity icin
        public string Username { get; set; } // Kullanıcı adı (Identity ile eşleşecek)
        public string? ProfilePictureUrl { get; set; } // Profil resmi
        public bool CanPost { get; set; } = true; // Post atma yetkisi
        public string Email { get; set; }
    }
}
