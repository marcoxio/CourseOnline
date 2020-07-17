using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using Dominio;
using Dominio.Entities;
using MediatR;
using Persistencia;

namespace Aplicacion.Courses
{
    public class ConsultId
    {
        public class UniqueCourse : IRequest<Course>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<UniqueCourse, Course>
        {
            private readonly CoursesOnlineContext _context;

            public Handler(CoursesOnlineContext context)
            {
                _context = context;
            }

            public async Task<Course> Handle(UniqueCourse request, CancellationToken cancellationToken)
            {
                var course = await _context.Course.FindAsync(request.Id);
                if(course == null)
                {
                    // throw new Exception("That course dont exists");
                    throw new HandlerException(HttpStatusCode.NotFound,new {message = "Don't found course"});
                }
                return course;
            }
        }
    }
}