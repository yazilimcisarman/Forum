using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Services
{
    public interface IAuthServices
    {
        Task<ApiResponse<object>> GenerateToken(LoginDto dto);
        Task<ApiResponse<object>> CheckUser(LoginDto dto);
        Task<ApiResponse<object>> CreateRole(string roleName);
        Task<ApiResponse<object>> AddRoleToUser(string email);
    }
}
