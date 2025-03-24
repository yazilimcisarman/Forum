using Forum.Application.Dtos.PostStatusDtos;
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
    public class PostStatusServices : IPostStatusServices
    {
        private readonly IGenericRepository<PostStatus> _repository;

        public PostStatusServices(IGenericRepository<PostStatus> repository)
        {
            _repository = repository;
        }

        public async Task CreatePostStatusAsync(CreatePostStatusDto postStatusDto)
        {
            var postStatus = new PostStatus
            {
                Name = postStatusDto.Name
            };

            await _repository.AddAsync(postStatus);
        }

        public async Task DeletePostStatusAsync(int postStatusId)
        {
            var postStatus = await _repository.GetByIdAsync(postStatusId);
            if (postStatus == null)
                throw new KeyNotFoundException("PostStatus bulunamadı.");

            await _repository.DeleteAsync(postStatus);
        }

        public async Task<List<ResultPostStatusDto>> GetAllPostStatusAsync()
        {
            var postStatuses = await _repository.GetAllAsync();
            return postStatuses.Select(ps => new ResultPostStatusDto
            {
                Id = ps.Id,
                Name = ps.Name
            }).ToList();
        }

        public async Task<GetByIdPostStatusDto> GetPostStatusByIdAsync(int postStatusId)
        {
            var postStatus = await _repository.GetByIdAsync(postStatusId);
            if (postStatus == null)
                throw new KeyNotFoundException("PostStatus bulunamadı.");

            return new GetByIdPostStatusDto
            {
                Id = postStatus.Id,
                Name = postStatus.Name
            };
        }

        public async Task UpdatePostStatusAsync(UpdatePostStatusDto postStatusDto)
        {
            var postStatus = await _repository.GetByIdAsync(postStatusDto.Id);
            if (postStatus == null)
                throw new KeyNotFoundException("PostStatus bulunamadı.");

            postStatus.Name = postStatusDto.Name;

            await _repository.UpdateAsync(postStatus);
        }
    }
}
