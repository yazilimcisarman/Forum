using FluentValidation;
using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Dtos.PostStatusDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Interfaces.Repositories;
using Forum.Application.Interfaces.Services;
using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class PostStatusServices : IPostStatusServices
    {
        private readonly IGenericRepository<PostStatus> _repository;
        private readonly IValidator<CreatePostStatusDto> _validator;
        private readonly IValidator<UpdatePostStatusDto> _updateValidator;

        public PostStatusServices(IGenericRepository<PostStatus> repository, IValidator<CreatePostStatusDto> validator, IValidator<UpdatePostStatusDto> updateValidator)
        {
            _repository = repository;
            _validator = validator;
            _updateValidator = updateValidator;
        }

        public async Task<ApiResponse<object>> CreatePostStatus(CreatePostStatusDto postStatusDto)
        {
            try
            {
                var validate = await _validator.ValidateAsync(postStatusDto);
                if (!validate.IsValid)
                {
                    return new ApiResponse<object> { Status = false, Data = postStatusDto, ErrorMessage = string.Join(", ", validate.Errors.Select(e => e.ErrorMessage)) };
                }
                var postStatus = new PostStatus
                {
                    Name = postStatusDto.Name
                };

                await _repository.AddAsync(postStatus);
                return new ApiResponse<object> { Status = true, Data = postStatus, Info = "Post Status Oluşturuldu." };

            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status=false, Data = null, ErrorMessage=ex.Message };
            }
           
        }

        public async Task<ApiResponse<object>> DeletePostStatus(int postStatusId)
        {
            try
            {
                var postStatus = await _repository.GetByIdAsync(postStatusId);
                if (postStatus == null)
                    return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = "Post Status Bulunamadı." };

                await _repository.DeleteAsync(postStatus);
                return new ApiResponse<object> { Status = true, Data = null, Info = "Post Status Silindi." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
            
        }

        public async Task<ApiResponse<List<ResultPostStatusDto>>> GetAllPostStatus()
        {
            try
            {
                var postStatuses = await _repository.GetAllAsync();
                if (postStatuses.Count == 0 || postStatuses == null)
                {
                    return new ApiResponse<List<ResultPostStatusDto>> { Status = true, Data = null, Info = "Post Status Bulunamadı." };
                }
                var result = postStatuses.Select(ps => new ResultPostStatusDto
                {
                    Id = ps.Id,
                    Name = ps.Name
                }).ToList();

                return new ApiResponse<List<ResultPostStatusDto>> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultPostStatusDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
            
        }

        public async Task<ApiResponse<GetByIdPostStatusDto>> GetByIdPostStatus(int postStatusId)
        {
            try
            {
                var postStatus = await _repository.GetByIdAsync(postStatusId);
                if (postStatus == null)
                    return new ApiResponse<GetByIdPostStatusDto> { Status = true, Data = null, Info = "Post Status Bulunamadı." };

                var result = new GetByIdPostStatusDto
                {
                    Id = postStatus.Id,
                    Name = postStatus.Name
                };
                return new ApiResponse<GetByIdPostStatusDto> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetByIdPostStatusDto> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<object>> UpdatePostStatus(UpdatePostStatusDto postStatusDto)
        {
            try
            {
                var validate = await _updateValidator.ValidateAsync(postStatusDto);
                if(!validate.IsValid)
                    return new ApiResponse<object> { Status = false, Data = postStatusDto, ErrorMessage = string.Join(", ", validate.Errors.Select(e => e.ErrorMessage)) };

                var postStatus = await _repository.GetByIdAsync(postStatusDto.Id);
                if(postStatus == null)
                    return new ApiResponse<object> { Status = true, Data = null, Info = "Post Status Bulunamadı." };


                postStatus.Name = postStatusDto.Name;

                await _repository.UpdateAsync(postStatus);
                return new ApiResponse<object> { Status = true, Data = postStatus, Info = "Post status Güncellendi."};
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
           
        }
    }
}
