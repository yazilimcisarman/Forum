using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Dtos.UserDtos;
using Forum.Application.Interfaces.Repositories;
using Forum.Domain.Entities;
using Forum.Persistence.Context.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Persistence.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<ForumIdentityUser> _userManager;  // ASP.NET Core dentity UserManager
        private readonly SignInManager<ForumIdentityUser> _signInManager; // ASP.NET Core Identity SignInManager
        private readonly RoleManager<ForumIdentityRole> _roleManager; // ASP.NET Core Identity RoleManager

        public IdentityRepository(UserManager<ForumIdentityUser> userManager, SignInManager<ForumIdentityUser> signInManager, RoleManager<ForumIdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterDto dto)
        {
            var identityuser = new ForumIdentityUser
            {
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                UserName = dto.UserName,

            };
            
            var result = await _userManager.CreateAsync(identityuser, dto.Password);
            

            return result;
        }

        public Task<IdentityResult> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetByIdIdentityUser> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;
            var result = new GetByIdIdentityUser
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
            };
            return result;
        }

        public async Task<GetByIdIdentityUser> GetUserByEmailAsync(string email)
        {
            var user= await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;
            var result = new GetByIdIdentityUser
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
            };
            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            var result = await _signInManager.PasswordSignInAsync(user,dto.Password,true,false);
            return result;

        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> CheckUser(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return false;

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return false;

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
                return false;

            var result = await _roleManager.CreateAsync(new ForumIdentityRole{ Name= roleName });
            if (result.Succeeded)
                return true;

            return false;
        }

        public async Task<bool> AddUserToRoleAsync(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            var role = new ForumIdentityRole
            {
                Name = roleName,
            };
            var roleExists = await _roleManager.RoleExistsAsync(role.Name);
            if (roleExists)
            {
                // Kullanıcıya rol ata
                var addToRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
                if (addToRoleResult.Succeeded)
                    return true;
            }
            return false;
        }

        public async Task<GetByIdIdentityUser> GetUserByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;

            var result = new GetByIdIdentityUser
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
            };
            return result;
        }


        //public Task<IdentityResult> UpdateUserAsync(User user)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
