using Forum.Application.Dtos.IdentityDtos;
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


        //public Task<IdentityResult> UpdateUserAsync(User user)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
