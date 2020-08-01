using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructors
{
    public class ConsultId
    {
          public class Execute : IRequest<InstructorModel>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Execute, InstructorModel>
        {
            private readonly IInstructor _instructorRepository;
            public Handler(IInstructor instructorRepository)
            {
                _instructorRepository = instructorRepository;

            }

            public Task<InstructorModel> Handle(Execute request, CancellationToken cancellationToken)
            {
                var instructor = _instructorRepository.ObtainById(request.Id);
                if (instructor == null)
                {
                    throw new HandlerException(HttpStatusCode.NotFound,new {message="dont found instructor"});
                }

                return instructor;
            }
        }
    }
}