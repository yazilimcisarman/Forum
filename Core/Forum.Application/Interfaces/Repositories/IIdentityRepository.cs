using Forum.Application.Dtos.IdentityDtos;
using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Repositories
{
    public interface IIdentityRepository 
    {
        Task<User> GetUserByIdAsync(string userId);             // Kullanıcıyı ID'ye göre getir
        Task<User> GetUserByUsernameAsync(string username);    // Kullanıcıyı kullanıcı adına göre getir
        Task<(bool success, string message)> CreateUserAsync(User user, string password);  // Kullanıcıyı oluştur
        Task<(bool success, string message)> UpdateUserAsync(User user);                  // Kullanıcıyı güncelle
        Task<(bool success, string message)> DeleteUserAsync(string userId);
        Task<string> LoginAsync(LoginDto dto);
        Task<string> RegisterAsync(RegisterDto dto);
        Task LogOutAsync();
    }
}
