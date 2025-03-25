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
        Task<GetByIdUserDto> GetByIdUser(int id);
        Task<List<ResultUserDto>> GetAllUsers();
        Task CreateUser(CreateUserDto user);
        Task UpdateUser(UpdateUserDto user);
        Task DeleteUser(int id);
    }
}
