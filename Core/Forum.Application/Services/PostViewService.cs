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
    public class PostViewService
        : IPostViewService
    {
        private readonly IPostViewRepository _postViewRepository;

        public PostViewService(IPostViewRepository postViewRepository)
        {
            _postViewRepository = postViewRepository;
        }

        public async Task<ApiResponse<object>> AddViewAsync(int postId, string userId)
        {
            try
            {
                var alreadyViewed = await _postViewRepository.IsPostViewedByUserAsync(postId, userId);
                if (alreadyViewed)
                {
                    return new ApiResponse<object>
                    {
                        Status = false,
                        ErrorMessage = "Bu post zaten görüntülenmiş."
                    };
                }

                await _postViewRepository.AddViewAsync(new PostView
                {
                    PostId = postId,
                    UserId = userId,
                    VisitorIdentifier = userId,
                    ViewedAt = DateTime.UtcNow
                });

                return new ApiResponse<object>
                {
                    Status = true,
                    Info = "Görüntüleme başarıyla kaydedildi."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    Status = false,
                    ErrorMessage = $"Görüntüleme eklenirken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<object>> GetViewCountAsync(int postId)
        {
            try
            {
                var count = await _postViewRepository.GetViewCountAsync(postId);
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
                    ErrorMessage = $"Görüntüleme sayısı alınırken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<object>> IsViewedAsync(int postId, string userId)
        {
            try
            {
                var viewed = await _postViewRepository.IsPostViewedByUserAsync(postId, userId);
                return new ApiResponse<object>
                {
                    Status = true,
                    Data = viewed
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    Status = false,
                    ErrorMessage = $"Görüntüleme durumu kontrol edilirken hata oluştu: {ex.Message}"
                };
            }
        }
    }
}
