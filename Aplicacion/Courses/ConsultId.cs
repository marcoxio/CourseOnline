using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using AutoMapper;
using Dominio;
using Dominio.DTOs;
using Dominio.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Courses
{
    public class ConsultId
    {
        public class UniqueCourse : IRequest<CourseDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<UniqueCourse, CourseDto>
        {
            private readonly CoursesOnlineContext _context;
            private readonly IMapper _mapper;

            public Handler(CoursesOnlineContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<CourseDto> Handle(UniqueCourse request, CancellationToken cancellationToken)
            {
                // var course = await _context.Course.FindAsync(request.Id);
                 var course = await _context.Course	
                .Include(x => x.ListComment)
                .Include(x => x.PromotionPrice)
                .Include(x => x.Instructor)
                .ThenInclude(y => y.Instructor)
                .FirstOrDefaultAsync(a => a.CourseId == request.Id);
                if (course == null)
                {
                    // throw new Exception("That course dont exists");
                    throw new HandlerException(HttpStatusCode.NotFound, new { message = "Don't found course" });
                }
                var courseDto = _mapper.Map<Course,CourseDto>(course);
                return courseDto;
            }
        }
    }
}