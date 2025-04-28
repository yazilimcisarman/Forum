using FluentValidation;
using FluentValidation.Results;
using Forum.Application.Dtos.CategoryDtos;
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
    public class CategoryServices : ICategoryServices
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IValidator<CreateCategoryDto> _validator;
        private readonly IValidator<UpdateCategoryDto> _updateValidator;
        private readonly ICategoryRepository _categoryRepository2;

        public CategoryServices(IGenericRepository<Category> categoryRepository, IValidator<CreateCategoryDto> validator, IValidator<UpdateCategoryDto> updateValidator, ICategoryRepository categoryRepository2)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
            _updateValidator = updateValidator;
            _categoryRepository2 = categoryRepository2;
        }

        public async Task<ApiResponse<List<ResultCategoryDto>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories.Count == 0 || categories == null)
                {
                    return new ApiResponse<List<ResultCategoryDto>> { Status = true, Data = null,Info= "Kategori Bulunamadı." };
                }
                var categoryDtos = new List<ResultCategoryDto>();

                foreach (var category in categories)
                {
                    categoryDtos.Add(new ResultCategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        PostCount = category.PostCount
                    });
                }

                return new ApiResponse<List<ResultCategoryDto>> { Status = true, Data = categoryDtos};
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultCategoryDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<GetByIdCategoryDto>> GetCategoryById(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return new ApiResponse<GetByIdCategoryDto> { Status = true, Data = null, Info = "Kategori Bulunamadı." };
                }
                var result = new GetByIdCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description= category.Description
                };
                return new ApiResponse<GetByIdCategoryDto> { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetByIdCategoryDto> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
            
        }

        public async Task<ApiResponse<object>> CreateCategory(CreateCategoryDto categoryDto)
        {
            try
            {
                var validator = await _validator.ValidateAsync(categoryDto);
                if (!validator.IsValid)
                {
                    return new ApiResponse<object> { Status = false, Data = categoryDto, ErrorMessage = string.Join(", ", validator.Errors.Select(e => e.ErrorMessage)) };
                }

                var category = new Category
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description,
                    PostCount = 0
                };
                
                await _categoryRepository.AddAsync(category);

                return new ApiResponse<object> { Status = true, Data = category,Info = "Kategori Oluşturuldu."};
                
                
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
            
        }

        public async Task<ApiResponse<object>> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            try
            {
                var validator = await _updateValidator.ValidateAsync(categoryDto);
                if (!validator.IsValid)
                {
                    return new ApiResponse<object> { Status = false, Data = categoryDto, ErrorMessage = string.Join(", ", validator.Errors.Select(e => e.ErrorMessage)) };
                }
                var category = new Category
                {
                    Id = categoryDto.Id,
                    Name = categoryDto.Name,
                    Description = categoryDto.Description,
                    PostCount = categoryDto.PostCount
                };

                await _categoryRepository.UpdateAsync(category);
                return new ApiResponse<object> { Status = true, Data = category,Info= "Kategori Güncellendi." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
            
        }

        public async Task<ApiResponse<object>> DeleteCategory(int categoryId)
        {
            try
            {
                var value = await _categoryRepository.GetByIdAsync(categoryId);
                if (value == null)
                    return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = "Kategori Bulunamadı." };
                await _categoryRepository.DeleteAsync(value);
                return new ApiResponse<object> { Status = true, Data = null, Info = "Kategori Silindi." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
           
        }

        public async Task<ApiResponse<object>> IncreasePostCount(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = "Kategori Bulunamadi" };
                }
                category.PostCount += 1;
                await _categoryRepository.UpdateAsync(category);
                return new ApiResponse<object> { Status = true, Data = null };
            }
            catch (Exception ex )
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<object>> DecreasePostCount(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = "Kategori Bulunamadi" };
                }
                category.PostCount -= 1;
                await _categoryRepository.UpdateAsync(category);
                return new ApiResponse<object> { Status = true, Data = null };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<List<ResultCategoryDto>>> GetHomePopularCategory()
        {
            try
            {
                var categories = await _categoryRepository2.GetHomePopularCategory();
                if (categories.Count == 0 || categories == null)
                {
                    return new ApiResponse<List<ResultCategoryDto>> { Status = true, Data = null, Info = "Kategori Bulunamadı." };
                }
                var categoryDtos = new List<ResultCategoryDto>();

                foreach (var category in categories)
                {
                    categoryDtos.Add(new ResultCategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        PostCount = category.PostCount,
                        ImageUrl = category.ImageUrl
                    });
                }

                return new ApiResponse<List<ResultCategoryDto>> { Status = true, Data = categoryDtos };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResultCategoryDto>> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }
    }
}
