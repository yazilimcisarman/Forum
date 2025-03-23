using Forum.Application.Dtos.CategoryDtos;
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
        Task<List<ResultCategoryDto>> GetAllCategoriesAsync(); // Tüm kategorileri al
        Task<GetByIdCategoryDto> GetCategoryByIdAsync(int categoryId); // ID ile kategori getir
        Task CreateCategoryAsync(CreateCategoryDto category); // Kategori oluştur
        Task UpdateCategoryAsync(UpdateCategoryDto category); // Kategori güncelle
        Task DeleteCategoryAsync(int categoryId); // Kategori sil
    }
}
