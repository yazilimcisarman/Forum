using AutoMapper;
using FluentValidation;
using Forum.Application.Dtos.PostDtos;
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
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, ResultUserDto>().ReverseMap();
            CreateMap<User, GetByIdUserDto>().ReverseMap();

            CreateMap<Post, CreatePostDto>().ReverseMap();
            CreateMap<Post, UpdatePostDto>().ReverseMap();
            CreateMap<Post, ResultPostDto>().ReverseMap();
            CreateMap<Post, GetByIdPostDto>().ReverseMap();

        }
    }
}
