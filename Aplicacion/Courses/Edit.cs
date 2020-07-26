using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using Dominio.Entities;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Courses
{
    public class Edit
    {
        public class Execute : IRequest{
            public Guid CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? DateOfPublication { get; set; }
            public List<Guid> ListInstructor { get; set; }
            public decimal? Price { get; set; }
            public decimal? Promotion { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.DateOfPublication).NotEmpty();
                RuleFor(x => x.Price).NotEmpty();
                RuleFor(x => x.Promotion).NotEmpty();
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
                
                var course = await _context.Course.FindAsync(request.CourseId);
                if(course == null)
                {
                    // throw new Exception("That course dont exists");
                     throw new HandlerException(HttpStatusCode.NotFound,new {message = "Don't found course"});
                }

                //Update list of instructors of course
                course.Title = request.Title ?? course.Title;
                course.Description = request.Description ?? course.Description;
                course.DateOfPublication = request.DateOfPublication ?? course.DateOfPublication;
                
                /*Update price of course*/
                var priceEntity = _context.Price
                .Where(x => x.CourseId == course.CourseId)
                .FirstOrDefault();

                if(priceEntity!=null){
                    priceEntity.Promotion = request.Promotion ?? priceEntity.Promotion;
                    priceEntity.ActualPrice = request.Price ?? priceEntity.ActualPrice;
                }else{
                    priceEntity = new Price{
                        PriceId = Guid.NewGuid(),
                        ActualPrice = request.Price ?? 0,
                        Promotion = request.Promotion ?? 0,
                        CourseId = course.CourseId
                    };
                    await _context.Price.AddAsync(priceEntity);
                }

                if(request.ListInstructor!=null)
                {
                    if(request.ListInstructor.Count>0)
                    {
                        //Delete current instructors at course of DB
                        var instructorsDB = _context.CourseInstructor.Where(x => x.CourseId == request.CourseId).ToList();
                        foreach (var instructorDelete in instructorsDB)
                        {
                            _context.CourseInstructor.Remove(instructorDelete);
                        }
                        
                        //add instructors
                         foreach (var ids in request.ListInstructor)
                        {
                            var newInstructor = new CourseInstructor {
                                CourseId = request.CourseId,
                                InstructorId = ids
                            };
                            await _context.CourseInstructor.AddAsync(newInstructor);
                        }
                    }
                }
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