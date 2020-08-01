using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructors
{
    public class Delete
    {
        public class Execute : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {

            private readonly IInstructor _instructorRepository;
            public Handler(IInstructor instructorRepository)
            {
                _instructorRepository = instructorRepository;

            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var results = await _instructorRepository.Delete(request.Id);
                if(results>0){
                    return Unit.Value;
                }

                throw new Exception("Something wrong, dont delete instructor");
            }
        }



    }
}