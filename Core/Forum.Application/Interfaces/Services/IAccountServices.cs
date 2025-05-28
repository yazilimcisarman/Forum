using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Services
{
    public interface IAccountServices
    {
        Task<ApiResponse<GetByIdIdentityUser>> GetUserById(string userId);
        Task<ApiResponse<GetByIdIdentityUser>> GetUserByEmail(string email);
        Task<ApiResponse<object>> CreateUser(RegisterDto user);
        Task<ApiResponse<object>> DeleteUser(string userId);
        Task<ApiResponse<object>> Login(LoginDto dto);
        Task<ApiResponse<object>> LogOut();
        Task<ApiResponse<GetByIdIdentityUser>> GetUserByUserName(string username);
    }
}
