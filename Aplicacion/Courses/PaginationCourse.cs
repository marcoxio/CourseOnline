using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.Pagination;
using Persistencia.Pagination.Interfaces;

namespace Aplicacion.Courses
{
    public class PaginationCourse
    {
        public class Execute : IRequest<PaginationModel>
        {
            public string Title { get; set; }
            public int NumberPage { get; set; }
            public int QuantityElements { get; set; }
        }

        public class Handler : IRequestHandler<Execute, PaginationModel>
        {
            private readonly IPagination _pagination;
            public Handler(IPagination pagination)
            {
                _pagination = pagination;
                
            }

            public async Task<PaginationModel> Handle(Execute request, CancellationToken cancellationToken)
            {
                var storeProcedure = "usp_obtain_pagination";
                var ordering = "Title";
                var parameters = new Dictionary<string,object>();
                parameters.Add("NameCourse", request.Title);

                return await _pagination.returnPagination(storeProcedure, request.NumberPage, request.QuantityElements,parameters,ordering);
            }
        }
    }
}