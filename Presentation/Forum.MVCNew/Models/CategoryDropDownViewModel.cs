using Forum.Application.Dtos.CategoryDtos;

namespace Forum.MVCNew.Models
{
    public class CategoryDropDownViewModel
    {
        public List<ResultCategoryDto> Categories { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}
