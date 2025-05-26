using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected string? JwtToken { get; private set; }
        protected ClaimsPrincipal? JwtClaimsPrincipal { get; private set; }

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                JwtToken = authHeader.Substring("Bearer ".Length).Trim();

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(JwtToken);

                var identity = new ClaimsIdentity(token.Claims, "jwt");
                JwtClaimsPrincipal = new ClaimsPrincipal(identity);
            }
        }

        // Kolay erişim için property'ler
        protected string? CurrentUserId => JwtClaimsPrincipal?.FindFirst("_u")?.Value;
        protected string? CurrentUserEmail => JwtClaimsPrincipal?.FindFirst("_e")?.Value;
        protected string? CurrentUserRole => JwtClaimsPrincipal?.FindFirst("role")?.Value;
    }
}
