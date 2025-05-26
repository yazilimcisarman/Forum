using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Forum.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected string? JwtToken { get; private set; }
        protected ClaimsPrincipal? JwtClaimsPrincipal { get; private set; }

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            JwtToken = _httpContextAccessor.HttpContext?.Request.Cookies["_t"];

            if (!string.IsNullOrEmpty(JwtToken))
            {
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
        protected string? tkn => JwtToken;
    }
}
