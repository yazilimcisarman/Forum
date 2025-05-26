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
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly IGenericRepository<SubComment> _subCommentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePostDto> _createPostValidator;
        private readonly IValidator<UpdatePostDto> _updatePostValidator;
        private readonly IPostRepository _postRepository;
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IGenericRepository<PostLike> _genericPostLikeRepository;
        private readonly IGenericRepository<PostView> _genericPostViewRepository;

        public PostServices(IGenericRepository<Post> repository, IMapper mapper, IValidator<CreatePostDto> createPostValidator, IGenericRepository<User> userRepository, IGenericRepository<Category> categoryRepository, IGenericRepository<PostStatus> postStatusRepository, IValidator<UpdatePostDto> updatePostValidator, IGenericRepository<Comment> commentRepository, IGenericRepository<SubComment> subCommentRepository, IPostRepository postRepository, IPostLikeRepository postLikeRepository, IGenericRepository<PostLike> genericPostLikeRepository,IGenericRepository<PostView> genericPostViewRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _createPostValidator = createPostValidator;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _postStatusRepository = postStatusRepository;
            _updatePostValidator = updatePostValidator;
            _commentRepository = commentRepository;
            _subCommentRepository = subCommentRepository;
            _postRepository = postRepository;
            _postLikeRepository = postLikeRepository;
            _genericPostLikeRepository = genericPostLikeRepository;
            _genericPostViewRepository = genericPostViewRepository;
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
            try
            {
                var post = await _repository.GetByIdAsync(postId);
                if (post == null)
                {
                    return new ApiResponse<object> { Status = true, Data = null, Info = "Post Bulunamadi." };
                }
                await _repository.DeleteAsync(post);
                return new ApiResponse<object> { Status = true, Data = null, Info = "Post Silindi." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<List<ResultPostDto>>> GetAllPosts()
        {
            try
            {
                var posts = await _repository.GetAllAsync();
                var users = await _userRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                var poststatus = await _postStatusRepository.GetAllAsync();
                var comments = await _commentRepository.GetAllAsync();
                var subcommnet = await _subCommentRepository.GetAllAsync();
                var like = await _genericPostLikeRepository.GetAllAsync();
                var view = await _genericPostViewRepository.GetAllAsync();
                if (posts ==null || posts.Count == 0)
                {
                    return new ApiResponse<List<ResultPostDto>> { Status = true, Data = null,Info="Post Bulunamadi." };
                }
                var result = _mapper.Map<List<ResultPostDto>>(posts);
                foreach (var postDto in result)
                {
                    postDto.LikeCount = like.Count(l => l.PostId == postDto.Id);
                }
                foreach (var postDto1 in result)
                {
                    postDto1.ViewCount = view.Count(l => l.PostId == postDto1.Id);
                }
                return new ApiResponse<List<ResultPostDto>> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultPostDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        //public async Task<ApiResponse<List<ResultPostDto>>> GetAllPostsInclude()
        //{
        //    try
        //    {
        //        var posts = await _repository.GetAllAsyncInclude(
        //            query =>
        //            query.Include(x => x.User)
        //                 .Include(x => x.Category)
        //                 .Include(x => x.Status)
        //                 .Include(x => x.Comments)
        //                    .ThenInclude(c => c.SubComments)
        //        );

        //        if (posts == null || posts.Count == 0)
        //        {
        //            return new ApiResponse<List<ResultPostDto>>
        //            {
        //                Status = true,
        //                Data = null,
        //                Info = "Post bulunamadı."
        //            };
        //        }

        //        var result = _mapper.Map<List<ResultPostDto>>(posts);

        //        return new ApiResponse<List<ResultPostDto>>
        //        {
        //            Status = true,
        //            Data = result
        //        };
        //    }
        //    catch (Exception ex)
        //    {

        //        return new ApiResponse<List<ResultPostDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
        //    }
        //}

        public async Task<ApiResponse<List<ResultPostDto>>> GetCountPosts(int count)
        {
            try
            {
                var posts = await _repository.GetTakeAsync(count);
                var users = await _userRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                var poststatus = await _postStatusRepository.GetAllAsync();
                var comments = await _commentRepository.GetAllAsync();
                if (posts == null || posts.Count == 0)
                {
                    return new ApiResponse<List<ResultPostDto>> { Status = true, Data = null, Info = "Post Bulunamadi." };
                }
                var result = _mapper.Map<List<ResultPostDto>>(posts);
                return new ApiResponse<List<ResultPostDto>> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultPostDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public Task<ApiResponse<List<ResultPostDto>>> GetPostByCategoryId(int categroyId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<GetByIdPostDto>> GetPostById(int postId)
        {
            try
            {
                var posts = await _repository.GetByIdAsync(postId);
                if (posts == null)
                {
                    return new ApiResponse<GetByIdPostDto> { Status = true, Data = null, Info = "Post Bulunamadi." };
                }
                var users = await _userRepository.GetAllAsync();
                var category = await _categoryRepository.GetByIdAsync(posts.CategoryId);
                var poststatus = await _postStatusRepository.GetByIdAsync(posts.StatusId);
                var comments = await _commentRepository.GetAllAsync();
                var likecount = await _postLikeRepository.GetLikeCountAsync(postId);

                var result = _mapper.Map<GetByIdPostDto>(posts);
                return new ApiResponse<GetByIdPostDto> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetByIdPostDto> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<List<DetailPostByCategoryDto>>> GetTwoPostsByCategoryId(int categoryId)
        {
            try
            {
                var posts = await _postRepository.GetTwoPostsByCategoryIdAsync(categoryId);
                //var users = await _userRepository.GetAllAsync();
                //var category = await _categoryRepository.GetAllAsync();
                //var poststatus = await _postStatusRepository.GetAllAsync();
                //var comments = await _commentRepository.GetAllAsync();
                //var subcommnet = await _subCommentRepository.GetAllAsync();
                if (posts == null || posts.Count == 0)
                {
                    return new ApiResponse<List<DetailPostByCategoryDto>> { Status = true, Data = null, Info = "Post Bulunamadi." };
                }
                var result = _mapper.Map<List<DetailPostByCategoryDto>>(posts);
                return new ApiResponse<List<DetailPostByCategoryDto>> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<DetailPostByCategoryDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<List<ResultPostDto>>> GetUserPosts(string userId)
        {
            try
            {
                var posts = await _postRepository.GetUserPosts(userId);
                var users = await _userRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                var poststatus = await _postStatusRepository.GetAllAsync();
                var comments = await _commentRepository.GetAllAsync();
                var subcommnet = await _subCommentRepository.GetAllAsync();
                if (posts == null || posts.Count == 0)
                {
                    return new ApiResponse<List<ResultPostDto>> { Status = true, Data = null, Info = "Post Bulunamadi." };
                }
                var result = _mapper.Map<List<ResultPostDto>>(posts);
                return new ApiResponse<List<ResultPostDto>> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultPostDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<object>> UpdatePost(UpdatePostDto dto)
        {
            try
            {
                var validate = _updatePostValidator.Validate(dto);
                if (!validate.IsValid)
                {
                    return new ApiResponse<object> {Status=false,Data= dto, ErrorMessage= string.Join(", ", validate.Errors.Select(e => e.ErrorMessage)) };
                }
                var post = await _repository.GetByIdAsync(dto.Id);
                if (post == null)
                {
                    return new ApiResponse<object> { Status = true, Data = null, Info = "Post Bulunamadi." };
                }

                var result = _mapper.Map(dto,post);
                await _repository.UpdateAsync(result);
                return new ApiResponse<object> {Status=true, Data = result,Info="Post Guncellendi." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }
    }
}
