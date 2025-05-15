using FluentValidation;
using Forum.Application.Interfaces.Repositories;
using Forum.Application.Interfaces.Services;
using Forum.Application.Mappings;
using Forum.Application.Services;
using Forum.Application.Validators.CategoryValidators;
using Forum.Application.Validators.CommentValidators;
using Forum.Application.Validators.PostStatuesValidators;
using Forum.Application.Validators.PostValidators;
using Forum.Application.Validators.SubCommentValidators;
using Forum.Application.Validators.UserValidators;
using Forum.Persistence.Context.Identity;
using Forum.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Persistence.Extensions
{
    public static class ServicesAdd
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<UserManager<ForumIdentityUser>>();
            services.AddScoped<SignInManager<ForumIdentityUser>>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IPostStatusServices, PostStatusServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IPostServices, PostServices>();
            services.AddScoped<ICommentServices, CommentServices>();
            services.AddScoped<ISubCommentServices, SubCommentServices>();
            services.AddScoped<IAuthServices, AuthServices>();


            services.AddValidatorsFromAssemblyContaining<CreateCategoryDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateCategoryDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CreatePostStatusDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateUserDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CreatePostDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatePostDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateCommentDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateCommentDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateSubCommentDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateSubCommentDtoValidator>();

            services.AddAutoMapper(typeof(GeneralMapping));
            return services;
        }
    }
}
