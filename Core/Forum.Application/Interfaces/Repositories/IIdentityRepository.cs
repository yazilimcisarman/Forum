using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Dtos.UserDtos;
using Forum.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Repositories
{
    public interface IIdentityRepository 
    {
        Task<GetByIdIdentityUser> GetUserByIdAsync(string userId);            
        Task<GetByIdIdentityUser> GetUserByEmailAsync(string email);
        Task<GetByIdIdentityUser> GetUserByUserName(string username);
        Task<IdentityResult> CreateUserAsync(RegisterDto user);  
        //Task<IdentityResult> UpdateUserAsync(User user);                  
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<SignInResult> LoginAsync(LoginDto dto);
        Task LogOutAsync();
        Task<bool> CheckUser(LoginDto dto);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AddUserToRoleAsync(string userId, string roleName);
    }
}
