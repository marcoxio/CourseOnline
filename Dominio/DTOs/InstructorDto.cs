using System;

namespace Dominio.DTOs
{
    public class InstructorDto
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public byte[] ProfilePhoto { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}