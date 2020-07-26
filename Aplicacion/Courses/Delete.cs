using System;
using System.Linq;
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
            public Guid Id { get; set; }
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
                //back list reference instructor of course
                var instructorSDB = _context.CourseInstructor.Where(x => x.CourseId == request.Id);
                foreach (var instructor in instructorSDB)
                {
                    _context.CourseInstructor.Remove(instructor);
                }

                 /*Delete comments for DB*/
                var commentsDB = _context.Comment.Where(x => x.CourseId == request.Id);
                foreach (var comment in commentsDB)
                {
                    _context.Comment.Remove(comment);
                }

                /*Delete price */
                var precioDB = _context.Price.Where(x => x .CourseId == request.Id).FirstOrDefault();
                if(precioDB!=null)
                {
                    _context.Price.Remove(precioDB);
                }

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