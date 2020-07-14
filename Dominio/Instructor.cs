using System.Collections.Generic;

namespace Dominio
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public ICollection<CourseInstructor> CourseLink { get; set; }
    }
}