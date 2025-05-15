using Forum.Application.Dtos.IdentityDtos;
using Forum.Application.Dtos.ResponseDtos;
using Forum.Application.Dtos.UserDtos;
using Forum.Application.Interfaces.Repositories;
using Forum.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IConfiguration _configuration;
        private readonly IIdentityRepository _identityRepository;

        public AuthServices(IConfiguration configuration, IIdentityRepository identityRepository)
        {
            _configuration = configuration;
            _identityRepository = identityRepository;
        }

        public async Task<ApiResponse<object>> CheckUser(LoginDto dto)
        {
            try
            {
                var result = await _identityRepository.CheckUser(dto);
                return new ApiResponse<object> { Status = result, Data = result, Info = "Kullanıcı Girişi Başarılı." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<object>> GenerateToken(LoginDto dto)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                     new Claim(JwtRegisteredClaimNames.Sub,dto.Email), //buraya user id atamasi yapilacak
                     //new Claim(ClaimTypes.Role,role),
                     new Claim("role","admin"),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 };

                var token = new JwtSecurityToken
                    (
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: credentials
                    );
                var resultToken = new JwtSecurityTokenHandler().WriteToken(token);
                return new ApiResponse<object> { Status = true, Data = resultToken, Info = "Token Oluşturuldu." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { Status = false, Data = null, ErrorMessage = ex.Message };
            }
            
        }
    }
}
