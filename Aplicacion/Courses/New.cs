using System;
using System.Collections.Generic;
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
            public Guid? CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? DateOfPublication { get; set; }

            public List<Guid> ListInstructor { get; set; }
             public decimal Price { get; set; }
            public decimal Promotion { get; set; }
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
                Guid _courseId = Guid.NewGuid();
                if (request.CourseId != null)
                {
                    _courseId = request.CourseId ?? Guid.NewGuid();
                }
               
                var course = new Course
                {
                    CourseId = _courseId,
                    Title = request.Title,
                    Description = request.Description,
                    DateOfPublication = request.DateOfPublication,
                    CreationDate = DateTime.UtcNow
                };

                await _context.Course.AddAsync(course);


                  if(request.ListInstructor != null){
                    
                    foreach (var id in request.ListInstructor)
                    {
                        var cursoInstructor = new CourseInstructor{
                            CourseId = _courseId,
                            InstructorId = id
                        };
                        await _context.CourseInstructor.AddAsync(cursoInstructor);
                    }
                }

                 /*Add logic insert price course*/
                var priceEntity = new Price{
                    CourseId = _courseId,
                    ActualPrice = request.Price,
                    Promotion = request.Promotion,
                    PriceId = Guid.NewGuid()

                };

                await _context.Price.AddAsync(priceEntity);

                
                var value = await _context.SaveChangesAsync();
                if(value > 0)
                {
                    return Unit.Value;
                    
                }

                throw new Exception("Someting wrong, dont insert course");

            }
        }
    }
}