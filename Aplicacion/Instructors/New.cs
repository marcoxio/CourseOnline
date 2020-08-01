using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructors
{
    public class New
    {
        public class Execute : IRequest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Grade { get; set; }
        }


        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Grade).NotEmpty();
            }
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
                var result = await _instructorRepository.New(request.FirstName,request.LastName,request.Grade);
                if(result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Someting wrong, dont save instructor");

            }
        }
    }
}