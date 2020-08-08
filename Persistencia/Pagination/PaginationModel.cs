using System.Collections.Generic;

namespace Persistencia.Pagination
{

    public class PaginationModel
    {
        public List<IDictionary<string,object>> ListRecords { get; set; }
        public int TotalRecords { get; set; }
        public int NumberPages { get; set; }
    }
}