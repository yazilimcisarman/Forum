using AutoMapper;
using FluentValidation;
using Forum.Application.Dtos.PostDtos;
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
    public class PostServices : IPostServices
    {
        private readonly IGenericRepository<Post> _repository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<PostStatus> _postStatusRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePostDto> _createPostValidator;

        public PostServices(IGenericRepository<Post> repository, IMapper mapper, IValidator<CreatePostDto> createPostValidator, IGenericRepository<User> userRepository, IGenericRepository<Category> categoryRepository, IGenericRepository<PostStatus> postStatusRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _createPostValidator = createPostValidator;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _postStatusRepository = postStatusRepository;
        }

        public async Task<ApiResponse<object>> CreatePost(CreatePostDto postDto)
        {
            try
            {
                var validation = _createPostValidator.Validate(postDto);
                if (!validation.IsValid)
                {
                    return new ApiResponse<object> { Status = false, Data = postDto, ErrorMessage = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage)) };
                }

                var post = _mapper.Map<Post>(postDto);
                await _repository.AddAsync(post);
                return new ApiResponse<object> { Status = true, Data = post, Info="Post Olusturuldu." };

            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<object>> DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<ResultPostDto>>> GetAllPosts()
        {
            try
            {
                var posts = await _repository.GetAllAsync();
                var users = await _userRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                var poststatus = await _postStatusRepository.GetAllAsync();
                if (posts ==null || posts.Count == 0)
                {
                    return new ApiResponse<List<ResultPostDto>> { Status = true, Data = null,Info="Post Bulunamadi." };
                }
                var result = _mapper.Map<List<ResultPostDto>>(posts);
                return new ApiResponse<List<ResultPostDto>> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultPostDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<GetByIdPostDto>> GetPostById(int postId)
        {
            try
            {
                var posts = await _repository.GetByIdAsync(postId);
                var users = await _userRepository.GetByIdAsync(posts.UserId);
                var category = await _categoryRepository.GetByIdAsync(posts.CategoryId);
                var poststatus = await _postStatusRepository.GetByIdAsync(posts.StatusId);
                if (posts == null)
                {
                    return new ApiResponse<GetByIdPostDto> { Status = true, Data = null, Info = "Post Bulunamadi." };
                }
                var result = _mapper.Map<GetByIdPostDto>(posts);
                return new ApiResponse<GetByIdPostDto> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetByIdPostDto> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public Task<ApiResponse<object>> UpdatePost(UpdatePostDto post)
        {
            throw new NotImplementedException();
        }
    }
}
