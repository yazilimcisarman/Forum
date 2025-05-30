using AutoMapper;
using FluentValidation;
using Forum.Application.Dtos.CommentDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Dtos.UserDtos;
using Forum.Application.Interfaces.Repositories;
using Forum.Application.Interfaces.Services;
using Forum.Domain.Entities;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<PostStatus> _postStatusRepository;
        private readonly IGenericRepository<SubComment> _subCommentRepository;
        private readonly IValidator<CreateCommentDto> _validator;
        private readonly IValidator<UpdateCommentDto> _updatevalidator;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository2;

        public CommentServices(IGenericRepository<Comment> commentRepository, IValidator<CreateCommentDto> validator, IMapper mapper, IValidator<UpdateCommentDto> updatevalidator, IGenericRepository<Post> postRepository, IGenericRepository<User> userRepository, IGenericRepository<Category> categoryRepository, IGenericRepository<PostStatus> postStatusRepository, IGenericRepository<SubComment> subCommentRepository, ICommentRepository commentRepository2)
        {
            _commentRepository = commentRepository;
            _validator = validator;
            _mapper = mapper;
            _updatevalidator = updatevalidator;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _postStatusRepository = postStatusRepository;
            _subCommentRepository = subCommentRepository;
            _commentRepository2 = commentRepository2;
        }

        public async Task<ApiResponse<object>> CreateComment(CreateCommentDto Comment)
        {
            try
            {
                var validate = _validator.Validate(Comment);
                if (!validate.IsValid)
                {
                    return new ApiResponse<object> { Status=false,Data=Comment, ErrorMessage = string.Join(", ", validate.Errors.Select(e => e.ErrorMessage)) };
                }
                var result = _mapper.Map<Comment>(Comment);
                await _commentRepository.AddAsync(result);
                return new ApiResponse<object> {Status=true,Data=Comment,Info="Yorum olusturuldu."};  

            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<object>> DeleteComment(int id)
        {
            try
            {
                var comment = await _commentRepository.GetByIdAsync(id);
                if (comment == null)
                {
                    return new ApiResponse<object> { Status = true, Data = null, Info = "Yorum bulunamadi." };
                }
                await _commentRepository.DeleteAsync(comment);
                return new ApiResponse<object> { Status = true, Data = null, Info = "Yorum silindi." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<List<ResultCommentDto>>> GetAllComments()
        {
            try
            {
                var comments = await _commentRepository.GetAllAsync();
                if (comments.Count == 0 || comments == null)
                {
                    return new ApiResponse<List<ResultCommentDto>> { Status = true, Data = null, Info = "Yorum Yok." };
                }
                var posts = await _postRepository.GetAllAsync();
                var users = await _userRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                var poststatus = await _postStatusRepository.GetAllAsync();

                var result = _mapper.Map<List<ResultCommentDto>>(comments);
                return new ApiResponse<List<ResultCommentDto>> { Status=true, Data = result };

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultCommentDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<List<ResultCommentsComponentDto>>> GetAllCommentsByPostId(int postId)
        {
            try
            {
                var comments = await _commentRepository2.GetAllCommentsByPostId(postId);
                if (comments.Count == 0 || comments == null)
                {
                    return new ApiResponse<List<ResultCommentsComponentDto>> { Status = true, Data = null, Info = "Yorum Yok." };
                }
                //var posts = await _postRepository.GetAllAsync();
                //var users = await _userRepository.GetAllAsync();
                //var category = await _categoryRepository.GetAllAsync();
                //var poststatus = await _postStatusRepository.GetAllAsync();

                var result = _mapper.Map<List<ResultCommentsComponentDto>>(comments);
                return new ApiResponse<List<ResultCommentsComponentDto>> { Status = true, Data = result };

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultCommentsComponentDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<GetByIdCommentDto>> GetByIdComment(int id)
        {
            try
            {
                var comment = await _commentRepository.GetByIdAsync(id);
                if(comment == null)
                {
                    return new ApiResponse<GetByIdCommentDto> { Status = true, Data = null, Info = "Yorum Bulunamadi." };
                }
                var posts = await _postRepository.GetAllAsync();
                var users = await _userRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                var poststatus = await _postStatusRepository.GetAllAsync();
                var subcomments = await _subCommentRepository.GetAllAsync();
                var result = _mapper.Map<GetByIdCommentDto>(comment);
                return new ApiResponse<GetByIdCommentDto> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetByIdCommentDto> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<List<ResultCommentDto>>> GetUserComments(string userId)
        {
            try
            {
                var comments = await _commentRepository2.GetUserComments(userId);
                if (comments.Count == 0 || comments == null)
                {
                    return new ApiResponse<List<ResultCommentDto>> { Status = true, Data = null, Info = "Yorum Yok." };
                }
                //var posts = await _postRepository.GetAllAsync();
                //var users = await _userRepository.GetAllAsync();
                //var category = await _categoryRepository.GetAllAsync();
                //var poststatus = await _postStatusRepository.GetAllAsync();

                var result = _mapper.Map<List<ResultCommentDto>>(comments);
                return new ApiResponse<List<ResultCommentDto>> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultCommentDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<object>> UpdateComment(UpdateCommentDto Comment)
        {
            try
            {
                var validate = _updatevalidator.Validate(Comment);
                if (!validate.IsValid)
                {
                    return new ApiResponse<object> { Status = false, Data = Comment, ErrorMessage = string.Join(", ", validate.Errors.Select(e => e.ErrorMessage)) };
                }
                var comment = await _commentRepository.GetByIdAsync(Comment.Id);
                if(comment == null)
                {
                    return new ApiResponse<object> { Status = true, Data = null, Info = "Yorum Bulunamadi." };
                }

                var result = _mapper.Map(Comment, comment);
                await _commentRepository.UpdateAsync(comment);
                return new ApiResponse<object> {Status= true, Data = result,Info="Yorum Guncellendi." };  
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }
    }
}
