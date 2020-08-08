using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Persistencia.Interfaces;
using Persistencia.Pagination.Interfaces;

namespace Persistencia.Pagination.Repository
{
    public class PaginationRepository : IPagination
    {
        private readonly IFactoryConnection _factoryConnection;
        public PaginationRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public async Task<PaginationModel> returnPagination(string storeProcedure, int numberPage, int quantityElements, IDictionary<string, object> parameterFilter, string sortedColumn)
        {
            PaginationModel paginationModel = new PaginationModel();
            List<IDictionary<string, object>> listReport = null;
            int totalRecords = 0;
            int totalPages = 0;
            try
            {
                var connection = _factoryConnection.GetConnection();
                DynamicParameters parameters = new DynamicParameters();
                
                foreach (var param in parameterFilter)
                {
                    parameters.Add("@" + param.Key, param.Value);
                }

                //Parameter In
                parameters.Add("@NumberPage", numberPage);
                parameters.Add("@QuantityElements", quantityElements);
                parameters.Add("@Sorted", sortedColumn);

                //
                 //Parameter Out
                parameters.Add("@TotalRecords", totalRecords, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@TotalPages", totalPages, DbType.Int32, ParameterDirection.Output);


                var result = await connection.QueryAsync(storeProcedure,parameters,commandType : CommandType.StoredProcedure);
                listReport = result.Select(x => (IDictionary<string,object>)x).ToList();
                paginationModel.ListRecords = listReport;
                paginationModel.NumberPages = parameters.Get<int>("@TotalPages");
                paginationModel.TotalRecords = parameters.Get<int>("@TotalRecords");
            }
            catch (Exception ex)
            {
                throw new Exception("Someting wrong, dont execute store procedure",ex);
            }
            finally
            {
                _factoryConnection.CloseConexion();
            }
            return paginationModel;
        }
    }
}