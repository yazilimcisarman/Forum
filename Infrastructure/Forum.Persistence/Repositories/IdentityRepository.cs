using Forum.Application.Interfaces.Repositories;
using Forum.Domain.Entities;
using Forum.Persistence.Context.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Persistence.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<ForumIdentityUser> _userManager;  // ASP.NET Core dentity UserManager
        private readonly SignInManager<ForumIdentityUser> _signInManager; // ASP.NET Core Identity SignInManager

        public IdentityRepository(UserManager<ForumIdentityUser> userManager, SignInManager<ForumIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Kullanıcıyı ID ile getir
        public async Task<ForumIdentityUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        // Kullanıcıyı kullanıcı adı ile getir
        public async Task<ForumIdentityUser> GetUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        // Kullanıcıyı oluştur
        public async Task<(bool success, string message)> CreateUserAsync(ForumIdentityUser user, string password)
        {
            // Kullanıcı adı veya e-posta ile daha önce oluşturulmuş bir kullanıcı olup olmadığını kontrol et
            var existingUserByUsername = await _userManager.FindByNameAsync(user.Name);
            if (existingUserByUsername != null)
                return (false, "Username is already taken.");

            var existingUserByEmail = await _userManager.FindByEmailAsync(user.Email);
            if (existingUserByEmail != null)
                return (false, "Email is already taken.");

            // Yeni kullanıcıyı oluştur
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                return (true, "User created successfully.");

            // Hata durumunda, hata mesajlarını dön
            return (false, string.Join(", ", result.Errors));
        }

        // Kullanıcıyı güncelle
        public async Task<(bool success, string message)> UpdateUserAsync(ForumIdentityUser user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null)
                return (false, "User not found.");

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return (true, "User updated successfully.");

            return (false, string.Join(", ", result.Errors));
        }

        // Kullanıcıyı sil
        public async Task<(bool success, string message)> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return (false, "User not found.");

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return (true, "User deleted successfully.");

            return (false, string.Join(", ", result.Errors));
        }

        Task<User> IIdentityRepository.GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        Task<User> IIdentityRepository.GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, string message)> CreateUserAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, string message)> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
