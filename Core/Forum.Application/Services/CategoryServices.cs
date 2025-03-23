using Forum.Application.Dtos.CategoryDtos;
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
    public class CategoryServices : ICategoryServices
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryServices(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDtos = new List<ResultCategoryDto>();

            foreach (var category in categories)
            {
                categoryDtos.Add(new ResultCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description=category.Description,
                    PostCount=category.PostCount
                });
            }

            return categoryDtos;
        }

        public async Task<GetByIdCategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null) return null;

            return new GetByIdCategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

      

        public async Task CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Description=categoryDto.Description,
                PostCount=0
            };

            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            var category = new Category
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                PostCount = categoryDto.PostCount
            };

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var value = await _categoryRepository.GetByIdAsync(categoryId);
            await _categoryRepository.DeleteAsync(value);
        }
    }
}
