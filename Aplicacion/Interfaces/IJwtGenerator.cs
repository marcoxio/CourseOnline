using Dominio.Entities;

namespace Aplicacion.Interfaces
{
    public interface IJwtGenerator
    {
         string CreateToken(User user);
    }
}