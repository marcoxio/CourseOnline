using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Courses
{
    public class Consult
    {
        public class ListCourses : IRequest<List<Course>> {}

        public class Handler : IRequestHandler<ListCourses, List<Course>>
        {
            private readonly CoursesOnlineContext _context;

            public Handler(CoursesOnlineContext context)
            {
                _context = context;
            }
            public async Task<List<Course>> Handle(ListCourses request, CancellationToken cancellationToken)
            {
                var courses = await _context.Course.ToListAsync();
                return courses;
            }
        }
    }
}