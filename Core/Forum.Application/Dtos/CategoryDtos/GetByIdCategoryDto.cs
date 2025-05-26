using Forum.Application.Dtos.PostDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.CategoryDtos
{
    public class GetByIdCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PostCount { get; set; }
        public string ImageUrl { get; set; }
        public string ColorId { get; set; }
        public List<CategoryDetailPostsDto> CategoryPosts{ get; set; }
    }
}
