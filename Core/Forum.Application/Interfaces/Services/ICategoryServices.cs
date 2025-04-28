using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Interfaces.Services
{
    public interface ICategoryServices
    {
        Task<ApiResponse<List<ResultCategoryDto>>> GetAllCategories(); // Tüm kategorileri al
        Task<ApiResponse<GetByIdCategoryDto>> GetCategoryById(int categoryId); // ID ile kategori getir
        Task<ApiResponse<object>> CreateCategory(CreateCategoryDto category); // Kategori oluştur
        Task<ApiResponse<object>> UpdateCategory(UpdateCategoryDto category); // Kategori güncelle
        Task<ApiResponse<object>> DeleteCategory(int categoryId); // Kategori sil
        Task<ApiResponse<object>> IncreasePostCount(int categoryId);
        Task<ApiResponse<object>> DecreasePostCount(int categoryId);
        Task<ApiResponse<List<ResultCategoryDto>>> GetHomePopularCategory(); 
    }
}
