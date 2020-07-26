using System.Linq;
using AutoMapper;
using Dominio.DTOs;
using Dominio.Entities;

namespace Aplicacion.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course,CourseDto>()
            .ForMember(x => x.Instructors, y => y.MapFrom(z => z.Instructor.Select(a => a.Instructor).ToList()))
            .ForMember(x => x.Comments, y => y.MapFrom(z => z.ListComment))
            .ForMember(x => x.Price, y => y.MapFrom(y => y.PromotionPrice));
            CreateMap<CourseInstructor,CourseInstructorDto>();
            CreateMap<Instructor,InstructorDto>();
            CreateMap<Comment,CommentDto>();
            CreateMap<Price,PriceDto>();
        }
    }
}