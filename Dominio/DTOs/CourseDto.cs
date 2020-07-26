using System;
using System.Collections.Generic;

namespace Dominio.DTOs
{
    public class CourseDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfPublication { get; set; }
        public byte[] CoverPhoto { get; set; }
        public ICollection<InstructorDto> Instructors { get; set; }
        public PriceDto Price { get; set; }
        public ICollection<CommentDto> Comments { get; set; }

    }
}