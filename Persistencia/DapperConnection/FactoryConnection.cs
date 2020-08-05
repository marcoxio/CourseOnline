using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Persistencia.Interfaces;

namespace Persistencia.DapperConnection
{
    public class FactoryConnection : IFactoryConnection
    {
        private  IDbConnection _connection;
        private readonly IOptions<ConnectConfiguration> _configs;
        public FactoryConnection(IOptions<ConnectConfiguration> configs)
        {
            _configs = configs;

        }

        public void CloseConexion()
        {
            if(_connection != null && _connection.State == ConnectionState.Open){
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if(_connection == null){
                _connection = new SqlConnection(_configs.Value.DefaultConnection);
            }
            if(_connection.State != ConnectionState.Open){
                _connection.Open();
            }

            return _connection;
        }
    }
}