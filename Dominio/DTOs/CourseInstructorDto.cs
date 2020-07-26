using System;

namespace Dominio.DTOs
{
    public class CourseInstructorDto
    {
        public Guid CourseId { get; set; }
        public Guid InstructorId { get; set; }
    }
}