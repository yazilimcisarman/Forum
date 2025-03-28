using AutoMapper;
using FluentValidation;
using Forum.Application.Dtos.CommentDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Dtos.SubCommentDtos;
using Forum.Application.Interfaces.Repositories;
using Forum.Application.Interfaces.Services;
using Forum.Domain.Entities;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class SubCommentServices : ISubCommentServices
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<PostStatus> _postStatusRepository;
        private readonly IGenericRepository<SubComment> _subCommentRepository;
        private readonly IValidator<CreateSubCommentDto> _validator;
        private readonly IValidator<UpdateSubCommentDto> _updatevalidator;
        private readonly IMapper _mapper;

        public SubCommentServices(IGenericRepository<Comment> commentRepository, IGenericRepository<Post> postRepository, IGenericRepository<User> userRepository, IGenericRepository<Category> categoryRepository, IGenericRepository<PostStatus> postStatusRepository, IValidator<CreateSubCommentDto> validator, IValidator<UpdateSubCommentDto> updatevalidator, IMapper mapper, IGenericRepository<SubComment> subCommentRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _postStatusRepository = postStatusRepository;
            _validator = validator;
            _updatevalidator = updatevalidator;
            _mapper = mapper;
            _subCommentRepository = subCommentRepository;
        }

        public async Task<ApiResponse<object>> CreateSubComment(CreateSubCommentDto SubComment)
        {
            try
            {
                var validate = _validator.Validate(SubComment);
                if (!validate.IsValid)
                {
                    return new ApiResponse<object> { Status = false, Data = SubComment, ErrorMessage = string.Join(", ", validate.Errors.Select(e => e.ErrorMessage)) };
                }
                var result = _mapper.Map<SubComment>(SubComment);
                await _subCommentRepository.AddAsync(result);
                return new ApiResponse<object> { Status = true, Data = SubComment, Info = "Yorum olusturuldu." };

            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<object>> DeleteSubComment(int id)
        {
            try
            {
                var comment = await _subCommentRepository.GetByIdAsync(id);
                if (comment == null)
                {
                    return new ApiResponse<object> { Status = true, Data = null, Info = "Yorum bulunamadi." };
                }
                await _subCommentRepository.DeleteAsync(comment);
                return new ApiResponse<object> { Status = true, Data = null, Info = "Yorum silindi." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<List<ResultSubCommentDto>>> GetAllSubComments()
        {
            try
            {
                var comments = await _subCommentRepository.GetAllAsync();
                if (comments.Count == 0 || comments == null)
                {
                    return new ApiResponse<List<ResultSubCommentDto>> { Status = true, Data = null, Info = "Yorum Yok." };
                }
                var posts = await _postRepository.GetAllAsync();
                var users = await _userRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                var poststatus = await _postStatusRepository.GetAllAsync();

                var result = _mapper.Map<List<ResultSubCommentDto>>(comments);
                return new ApiResponse<List<ResultSubCommentDto>> { Status = true, Data = result };

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultSubCommentDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<GetByIdSubCommentDto>> GetByIdSubComment(int id)
        {
            try
            {
                var comment = await _subCommentRepository.GetByIdAsync(id);
                if (comment == null)
                {
                    return new ApiResponse<GetByIdSubCommentDto> { Status = true, Data = null, Info = "Yorum Bulunamadi." };
                }
                var posts = await _postRepository.GetAllAsync();
                var users = await _userRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                var poststatus = await _postStatusRepository.GetAllAsync();
                var result = _mapper.Map<GetByIdSubCommentDto>(comment);
                return new ApiResponse<GetByIdSubCommentDto> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetByIdSubCommentDto> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<object>> UpdateSubComment(UpdateSubCommentDto SubComment)
        {
            try
            {
                var validate = _updatevalidator.Validate(SubComment);
                if (!validate.IsValid)
                {
                    return new ApiResponse<object> { Status = false, Data = SubComment, ErrorMessage = string.Join(", ", validate.Errors.Select(e => e.ErrorMessage)) };
                }
                var comment = await _subCommentRepository.GetByIdAsync(SubComment.Id);
                if (comment == null)
                {
                    return new ApiResponse<object> { Status = true, Data = null, Info = "Yorum Bulunamadi." };
                }

                var result = _mapper.Map(SubComment, comment);
                await _subCommentRepository.UpdateAsync(comment);
                return new ApiResponse<object> { Status = true, Data = result, Info = "Yorum Guncellendi." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }
    }
}
