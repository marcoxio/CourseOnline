using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.Pagination.Interfaces
{
    public interface IPagination
    {
         Task<PaginationModel> returnPagination(
            string storeProcedure,
            int numberPage,
            int quantityElements,
            IDictionary<string,object> parameterFilter,
            string sortedColumn);
    }
}