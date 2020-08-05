using System;
using System.Collections.Generic;

namespace Dominio.Entities
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfPublication { get; set; }
        public byte[] CoverPhoto { get; set; }
        public DateTime? CreationDate { get; set; }
        public Price PromotionPrice { get; set; }
        public ICollection<Comment> ListComment { get; set; }
        public ICollection<CourseInstructor> Instructor { get; set; }
    }
}