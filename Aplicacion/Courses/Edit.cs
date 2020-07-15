using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;

namespace Aplicacion.Courses
{
    public class Edit
    {
        public class Execute : IRequest{
            public int CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? DateOfPublication { get; set; }
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
                var course = await _context.Course.FindAsync(request.CourseId);
                if(course == null)
                {
                    throw new Exception("That course dont exists");
                }

                course.Title = request.Title ?? course.Title;
                course.Description = request.Description ?? course.Description;
                course.DateOfPublication = request.DateOfPublication ?? course.DateOfPublication;

                var result = await _context.SaveChangesAsync();
                if(result > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Dont save changes at course");


            }
        }
    }
}