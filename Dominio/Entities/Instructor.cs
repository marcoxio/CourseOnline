using System;
using System.Collections.Generic;

namespace Dominio.Entities
{
    public class Instructor
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public byte[] ProfilePhoto { get; set; }

        public DateTime? CreationDate { get; set; }
        public ICollection<CourseInstructor> Course { get; set; }
    }
}