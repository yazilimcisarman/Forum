using Forum.Application.Dtos.UserDtos;
using Forum.Application.Interfaces.Repositories;
using Forum.Application.Interfaces.Services;
using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class UserServices :IUserServices
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserServices(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        // Get by Id
        public async Task<GetByIdUserDto> GetByIdUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            // Manual mapping from User to GetByIdUserDto
            var userDto = new GetByIdUserDto
            {
                Id = user.Id,
                Username = user.Username,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Email = user.Email,
                UserIdentityId = user.UserIdentityId,   
                CanPost = user.CanPost,
                RegisteredAt = user.RegisteredAt
            };

            return userDto;
        }

        // Get All Users
        public async Task<List<ResultUserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();

            // Manual mapping from User to ResultUserDto
            var usersDto = users.Select(user => new ResultUserDto
            {
                Id = user.Id,
                Username = user.Username,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Email = user.Email,
                RegisteredAt= user.RegisteredAt,
                CanPost= user.CanPost,
                 UserIdentityId= user.UserIdentityId,
            }).ToList();

            return usersDto;
        }

        // Create User
        public async Task CreateUser(CreateUserDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                ProfilePictureUrl = userDto.ProfilePictureUrl,
                Email = userDto.Email,
                UserIdentityId = userDto.UserIdentityId,
            };

            await _userRepository.AddAsync(user);
        }

        // Update User
        public async Task UpdateUser(UpdateUserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);
            if (user == null)
            {
                return;
            }

            user.Username = userDto.Username;
            user.ProfilePictureUrl = userDto.ProfilePictureUrl;
            user.Email = userDto.Email;
            userDto.RegisteredAt = userDto.RegisteredAt;
            userDto.CanPost = userDto.CanPost;

            await _userRepository.UpdateAsync(user);
        }

        // Delete User
        public async Task DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return;
            }

            await _userRepository.DeleteAsync(user);
        }

    }
}
