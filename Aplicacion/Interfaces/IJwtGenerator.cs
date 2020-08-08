using System.Collections.Generic;
using Dominio.Entities;

namespace Aplicacion.Interfaces
{
    public interface IJwtGenerator
    {
         string CreateToken(User user, List<string> roles);
    }
}