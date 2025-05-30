using AutoMapper;
using FluentValidation;
using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Dtos.UserDtos;
using Forum.Application.Interfaces.Repositories;
using Forum.Application.Interfaces.Services;
using Forum.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserDto> _validator;
        private readonly IValidator<UpdateUserDto> _updateValidator;
        private readonly IUserRepository _userRepositoryCustom;

        public UserServices(IGenericRepository<User> userRepository, IMapper mapper, IValidator<CreateUserDto> validator, IValidator<UpdateUserDto> updateValidator, IUserRepository userRepositoryCustom)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
            _updateValidator = updateValidator;
            _userRepositoryCustom = userRepositoryCustom;
        }

        // Get by Id
        public async Task<ApiResponse<GetByIdUserDto>> GetByIdUser(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return new ApiResponse<GetByIdUserDto> { Status = true, Data = null, Info = "Kullanıcı Bulunamadı." };
                }
                var result = _mapper.Map<GetByIdUserDto>(user);

                return new ApiResponse<GetByIdUserDto> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetByIdUserDto> { Status=false,Data=null,ErrorMessage=ex.Message };
            }
        }

        // Get All Users
        public async Task<ApiResponse<List<ResultUserDto>>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                if (users.Count == 0 || users == null)
                {
                    return new ApiResponse<List<ResultUserDto>> { Status = true, Data = null, Info = "Kullanıcı Bulunamadı." };
                }
                var result = _mapper.Map<List<ResultUserDto>>(users);
                return new ApiResponse<List<ResultUserDto>>{ Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultUserDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        // Create User
        public async Task<ApiResponse<object>> CreateUser(CreateUserDto userDto)
        {
            try
            {
                var validator = await _validator.ValidateAsync(userDto);
                if (!validator.IsValid)
                {
                    return new ApiResponse<object> { Status = false, Data = userDto, ErrorMessage = string.Join(", ", validator.Errors.Select(e => e.ErrorMessage)) };
                }
                var result = _mapper.Map<User>(userDto);
                await _userRepository.AddAsync(result);
                return new ApiResponse<object> { Status = true, Data = null, Info = "Kullanıcı Oluşturuldu." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        // Update User
        public async Task<ApiResponse<object>> UpdateUser(UpdateUserDto userDto)
        {
            try
            {
                var validate = await _updateValidator.ValidateAsync(userDto);
                if (!validate.IsValid)
                {
                    return new ApiResponse<object> { Status = false, Data = userDto, ErrorMessage = string.Join(", ", validate.Errors.Select(e => e.ErrorMessage)) };
                }
                var user = await _userRepository.GetByIdAsync(userDto.Id);
                if (user == null)
                {
                    return new ApiResponse<object> { Status = true, Data = null, Info = "Kullanıcı Bulunamadı." };
                }
                var result = _mapper.Map(userDto,user);
                await _userRepository.UpdateAsync(result);
                return new ApiResponse<object> { Status = true, Data = null, Info = "Kullanıcı Güncellendi." };
            }
            catch (Exception ex )
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        // Delete User
        public async Task<ApiResponse<object>> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return new ApiResponse<object> { Status = true, Data = null, Info = "Kullanıcı Bulunamadı." };
                }

                await _userRepository.DeleteAsync(user);
                return new ApiResponse<object> { Status = true, Data = null, Info = "Kullanici silindi." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<List<BestUsersTakeThreeDto>>> GetBestUsersTakeThree()
        {
            try
            {
                var users = await _userRepositoryCustom.GetBestUsersTakeThree();
                if (users.Count == 0 || users == null)
                {
                    return new ApiResponse<List<BestUsersTakeThreeDto>> { Status = true, Data = null, Info = "Kullanıcı Bulunamadı." };
                }
                var result = _mapper.Map<List<BestUsersTakeThreeDto>>(users);
                return new ApiResponse<List<BestUsersTakeThreeDto>> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<BestUsersTakeThreeDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<GetByIdUserDto>> GetByUserIdentityId(string userIdentityId)
        {
            try
            {
                var user = await _userRepositoryCustom.GetUserByIdentityId(userIdentityId);
                if (user == null)
                {
                    return new ApiResponse<GetByIdUserDto> { Status = true, Data = null, Info = "Kullanıcı Bulunamadı." };
                }
                var result = _mapper.Map<GetByIdUserDto>(user);

                return new ApiResponse<GetByIdUserDto> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetByIdUserDto> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        //public async Task<ApiResponse<object>> CheckUser(LoginDto dto)
        //{
        //    try
        //    {
        //        var result = await _userRepositoryCustom.(dto);
        //    }
        //    catch (Exception ex )
        //    {
        //        return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
        //    }
        //}
    }
}
