using System;

namespace Persistencia.DapperConnection.Instructor
{
    public class InstructorModel
    {
        public Guid InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
    }
}