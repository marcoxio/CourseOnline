using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using Dominio;
using Dominio.Entities;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Courses
{
    public class New
    {
        public class Execute : IRequest {
            // public int CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? DateOfPublication { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.DateOfPublication).NotEmpty();
            }
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
                var course = new Course
                {
                    Title = request.Title,
                    Description = request.Description,
                    DateOfPublication = request.DateOfPublication
                };

                await _context.Course.AddAsync(course);
                var value = await _context.SaveChangesAsync();
                if(value > 0)
                {
                    return Unit.Value;
                    throw new HandlerException(HttpStatusCode.Created,new {message = "Course Created"});
                }

                throw new Exception("Someting wrong, dont insert course");

            }
        }
    }
}