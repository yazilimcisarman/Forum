using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Interfaces.Repositories;
using Forum.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IIdentityRepository _identityRepository;

        public AccountServices(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<ApiResponse<object>> CreateUser(RegisterDto user)
        {
            try
            {
                var checkuser = await _identityRepository.GetUserByEmailAsync(user.Email);
                if (checkuser != null)
                {
                    return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = "Kullanici Mevcut" };
                }

                var result = await _identityRepository.CreateUserAsync(user);
                if (result.Succeeded)
                {
                    return new ApiResponse<object> { Status = true, Data = null, Info = "Kullanici Olusturuldu." };
                }
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = string.Join(", ", result.Errors.Select(e => e.Description))}; 
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public Task<ApiResponse<object>> DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<GetByIdIdentityUser>> GetUserByEmail(string email)
        {
            try
            {
                var user = await _identityRepository.GetUserByEmailAsync(email);
                if (user == null)
                {
                    return new ApiResponse<GetByIdIdentityUser> { Status = true, Data = null, Info = "Kullanici Bulunamadi" };
                }
                return new ApiResponse<GetByIdIdentityUser> { Status=true,Data = user };
            }
            catch (Exception ex )
            {
                return new ApiResponse<GetByIdIdentityUser> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<GetByIdIdentityUser>> GetUserById(string userId)
        {
            try
            {
                var user = await _identityRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return new ApiResponse<GetByIdIdentityUser> { Status = true, Data = null, Info = "Kullanici Bulunamadi" };
                }
                return new ApiResponse<GetByIdIdentityUser> { Status = true, Data = user };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetByIdIdentityUser> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<object>> Login(LoginDto dto)
        {
            try
            {
                var result = await _identityRepository.LoginAsync(dto);
                if (result.Succeeded)
                {
                    return new ApiResponse<object> { Status = true, Data = null, Info = "Kullanici Girisi Basarili" };
                }
                return new ApiResponse<object> { Status=false, Data = null,ErrorMessage="Kullanici girisi hatasi, email ve sifrenizi kontrol ediniz"};
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<object>> LogOut()
        {
            try
            {
                await _identityRepository.LogOutAsync();
                return new ApiResponse<object> { Status = true, Data = null, Info = "Kullanici Cikis Yapti," };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }
    }
}
