using Microsoft.AspNetCore.Identity;

namespace Dominio.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}