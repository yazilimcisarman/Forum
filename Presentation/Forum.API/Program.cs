using FluentValidation;
using Forum.Application.Dtos.CategoryDtos;
using Forum.Application.Interfaces.Repositories;
using Forum.Application.Interfaces.Services;
using Forum.Application.Mappings;
using Forum.Application.Services;
using Forum.Application.Validators;
using Forum.Application.Validators.CategoryValidators;
using Forum.Application.Validators.CommentValidators;
using Forum.Application.Validators.PostStatuesValidators;
using Forum.Application.Validators.PostValidators;
using Forum.Application.Validators.SubCommentValidators;
using Forum.Application.Validators.UserValidators;
using Forum.Persistence.Context;
using Forum.Persistence.Context.Identity;
using Forum.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddDbContext<ForumDbContext>();
builder.Services.AddDbContext<ForumIdentityDbContext>();
builder.Services.AddIdentity<ForumIdentityUser, ForumIdentityRole>()
    .AddEntityFrameworkStores<ForumIdentityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllers();
builder.Services.AddScoped<UserManager<ForumIdentityUser>>();
builder.Services.AddScoped<SignInManager<ForumIdentityUser>>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IPostStatusServices, PostStatusServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IPostServices, PostServices>();
builder.Services.AddScoped<ICommentServices, CommentServices>();
builder.Services.AddScoped<ISubCommentServices, SubCommentServices>();


builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePostStatusDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePostDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdatePostDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCommentDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCommentDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateSubCommentDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateSubCommentDtoValidator>();

builder.Services.AddAutoMapper(typeof(GeneralMapping));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();
builder.Services.AddEndpointsApiExplorer();
// Configure the HTTP request pipeline.
app.MapScalarApiReference(
    opt => {
        opt.Title = "Forum v1";
        opt.Theme = ScalarTheme.BluePlanet;
        opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    }
);

app.MapOpenApi();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
