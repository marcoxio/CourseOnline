using System.Data;

namespace Persistencia.Interfaces
{
    public interface IFactoryConnection
    {
        void CloseConexion();
         IDbConnection GetConnection();
    }
}