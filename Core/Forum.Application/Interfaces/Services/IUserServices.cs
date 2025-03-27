using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Dtos.UserDtos;
using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Services
{
    public interface IUserServices
    {
        Task<ApiResponse<GetByIdUserDto>> GetByIdUser(int id);
        Task<ApiResponse<List<ResultUserDto>>> GetAllUsers();
        Task<ApiResponse<object>>CreateUser(CreateUserDto user);
        Task<ApiResponse<object>> UpdateUser(UpdateUserDto user);
        Task<ApiResponse<object>> DeleteUser(int id);
    }
}
