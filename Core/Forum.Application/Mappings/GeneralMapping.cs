using AutoMapper;
using FluentValidation;
using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Dtos.CommentDtos;
using Forum.Application.Dtos.PostDtos;
using Forum.Application.Dtos.PostStatusDtos;
using Forum.Application.Dtos.SubCommentDtos;
using Forum.Application.Dtos.UserDtos;
using Forum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, GetByIdCategoryDto>().ReverseMap();

            CreateMap<PostStatus, CreatePostStatusDto>().ReverseMap();
            CreateMap<PostStatus, UpdatePostStatusDto>().ReverseMap();
            CreateMap<PostStatus, ResultPostStatusDto>().ReverseMap();
            CreateMap<PostStatus, GetByIdPostStatusDto>().ReverseMap();


            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, ResultUserDto>().ReverseMap();
            CreateMap<User, GetByIdUserDto>().ReverseMap();

            CreateMap<Post, CreatePostDto>().ReverseMap();
            CreateMap<Post, UpdatePostDto>().ReverseMap();
            CreateMap<Post, ResultPostDto>().ReverseMap();
            CreateMap<Post, GetByIdPostDto>().ReverseMap();
            CreateMap<Post, CommentViewPostDto>().ReverseMap();

            CreateMap<Comment, CreateCommentDto>().ReverseMap();
            CreateMap<Comment, UpdateCommentDto>().ReverseMap();
            CreateMap<Comment, ResultCommentDto>().ReverseMap();
            CreateMap<Comment, GetByIdCommentDto>().ReverseMap();
            CreateMap<Comment, PostViewCommentDto>().ReverseMap();


            CreateMap<SubComment, CreateSubCommentDto>().ReverseMap();
            CreateMap<SubComment, UpdateSubCommentDto>().ReverseMap();
            CreateMap<SubComment, ResultSubCommentDto>().ReverseMap();
            CreateMap<SubComment, GetByIdSubCommentDto>().ReverseMap();

        }
    }
}
