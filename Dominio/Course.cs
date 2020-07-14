using System;

namespace Dominio
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfPublication { get; set; }
        public byte[] CoverPhoto { get; set; }
    }
}