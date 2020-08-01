using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConnection.Instructor
{
    public interface IInstructor
    {
         Task<IEnumerable<InstructorModel>> ObtainList();
         Task<InstructorModel> ObtainById(Guid id);

         Task<int> New(string firstName,string lastName,string grade);

         Task<int> Update(Guid instructorId,string firstName,string lastName,string grade);

         Task<int> Delete(Guid id);
    }
}