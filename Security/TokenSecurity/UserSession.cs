using System.Linq;
using System.Security.Claims;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Security
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccesor;
        public UserSession(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public string ObtainUserSession()
        {
               //username Current
            var userName = _httpContextAccesor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userName;
        }
    }
}