using System.Collections.Generic;
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
    public class Consult
    {
        public class ListCourses : IRequest<List<CourseDto>> { }

        public class Handler : IRequestHandler<ListCourses, List<CourseDto>>
        {
            private readonly CoursesOnlineContext _context;
            private readonly IMapper _mapper;

            public Handler(CoursesOnlineContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<List<CourseDto>> Handle(ListCourses request, CancellationToken cancellationToken)
            {
                var courses = await _context.Course
                .Include(x => x.ListComment)
                .Include(x => x.PromotionPrice)
                // CourseIntructor
                .Include(x => x.Instructor)
                .ThenInclude(x => x.Instructor)
                .ToListAsync();
                var coursesDto = _mapper.Map<List<Course>,List<CourseDto>>(courses);
                return coursesDto;

                
            }
        }
    }
}