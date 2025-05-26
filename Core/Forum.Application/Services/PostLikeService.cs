using Forum.Application.Dtos.PostLikeDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Interfaces.Repositories;
using Forum.Application.Interfaces.Services;
using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository _likeRepository;

        public PostLikeService(IPostLikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<ApiResponse<object>> AddLikeAsync(CreatePostLikeDto dto)
        {
            try
            {
                var alreadyLiked = await _likeRepository.IsPostLikedByUserAsync(dto.PostId, dto.UserId);
                if (alreadyLiked)
                {
                    return new ApiResponse<object>
                    {
                        Status = false,
                        ErrorMessage = "Bu post zaten beğenilmiş."
                    };
                }

                await _likeRepository.AddLikeAsync(new PostLike
                {
                    PostId = dto.PostId,
                    UserId = dto.UserId,
                    LikedAt = DateTime.UtcNow
                });

                return new ApiResponse<object>
                {
                    Status = true,
                    Info = "Beğeni başarıyla eklendi."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    Status = false,
                    ErrorMessage = $"Beğeni eklenirken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<object>> IsLikedAsync(int postId, int userId)
        {
            try
            {
                var liked = await _likeRepository.IsPostLikedByUserAsync(postId, userId);
                return new ApiResponse<object>
                {
                    Status = true,
                    Data = liked
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    Status = false,
                    ErrorMessage = $"Beğeni durumu kontrol edilirken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<object>> LikeCountAsync(int postId)
        {
            try
            {
                var count = await _likeRepository.GetLikeCountAsync(postId);
                return new ApiResponse<object>
                {
                    Status = true,
                    Data = count
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    Status = false,
                    ErrorMessage = $"Beğeni sayısı alınırken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<object>> RemoveLikeAsync(DeletePostLikeDto dto)
        {
            try
            {
                var like = await _likeRepository.GetLikeAsync(dto.PostId, dto.UserId);
                if (like == null)
                {
                    return new ApiResponse<object>
                    {
                        Status = false,
                        ErrorMessage = "Beğeni bulunamadı."
                    };
                }

                await _likeRepository.RemoveLikeAsync(like);

                return new ApiResponse<object>
                {
                    Status = true,
                    Info = "Beğeni başarıyla kaldırıldı."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    Status = false,
                    ErrorMessage = $"Beğeni kaldırılırken hata oluştu: {ex.Message}"
                };
            }
        }
        public async Task<ApiResponse<object>> ToggleLikeAsync(CreatePostLikeDto dto)
        {
            try
            {
                var alreadyLiked = await _likeRepository.IsPostLikedByUserAsync(dto.PostId, dto.UserId);

                if (alreadyLiked)
                {
                    var like = await _likeRepository.GetLikeAsync(dto.PostId, dto.UserId);
                    if (like != null)
                    {
                        await _likeRepository.RemoveLikeAsync(like);

                        return new ApiResponse<object>
                        {
                            Status = true,
                            Info = "Beğeni kaldırıldı."
                        };
                    }
                    else
                    {
                        return new ApiResponse<object>
                        {
                            Status = false,
                            ErrorMessage = "Beğeni bulunamadı."
                        };
                    }
                }
                else
                {
                    await _likeRepository.AddLikeAsync(new PostLike
                    {
                        PostId = dto.PostId,
                        UserId = dto.UserId,
                        LikedAt = DateTime.UtcNow
                    });

                    return new ApiResponse<object>
                    {
                        Status = true,
                        Info = "Beğeni eklendi."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    Status = false,
                    ErrorMessage = $"Beğeni işlemi sırasında hata oluştu: {ex.Message}"
                };
            }
        }

    }
}
