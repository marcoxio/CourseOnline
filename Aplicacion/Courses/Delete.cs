using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using MediatR;
using Persistencia;

namespace Aplicacion.Courses
{
    public class Delete
    {
        public class Execute : IRequest{
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly CoursesOnlineContext _context;

            public Handler(CoursesOnlineContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var course = await _context.Course.FindAsync(request.Id);
                if(course==null){
                    // throw new Exception("Dont delete this course");
                    throw new HandlerException(HttpStatusCode.NotFound,new {message = "Don't found course"});
                }

                _context.Remove(course);
                var result = await _context.SaveChangesAsync();
                if(result> 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Someting wrong, dont save changes");

            }
        }
    }
}