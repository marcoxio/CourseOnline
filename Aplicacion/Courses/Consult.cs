using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;

namespace Aplicacion.Courses
{
    public class Consult
    {
        public class ListCourses : IRequest<List<Course>> {}

        public class Handler : IRequestHandler<ListCourses, List<Course>>
        {
            public Task<List<Course>> Handle(ListCourses request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}