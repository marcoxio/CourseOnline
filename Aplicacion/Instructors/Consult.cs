using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructors
{
    public class Consult
    {
         public class ListInstructor : IRequest<List<InstructorModel>>{}

        public class Handler : IRequestHandler<ListInstructor, List<InstructorModel>>
        {
            private readonly IInstructor _instructorRepository;
            public Handler(IInstructor instructorRepository)
            {
                _instructorRepository=instructorRepository;
            }
        

            public async Task<List<InstructorModel>> Handle(ListInstructor request, CancellationToken cancellationToken)
            {
                 var resultado = await _instructorRepository.ObtainList();
                return resultado.ToList();
            }
        }
    }
}