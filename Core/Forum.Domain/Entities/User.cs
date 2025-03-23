using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserIdentityId { get; set; } //identity icin
        public string Username { get; set; } // Kullanıcı adı (Identity ile eşleşecek)
        public string? ProfilePictureUrl { get; set; } // Profil resmi
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
        public bool CanPost { get; set; } = true; // Post atma yetkisi
        public string Email { get; set; }
    }
}
