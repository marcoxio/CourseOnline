using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Security.Rol
{
    public class RolDelete
    {
        public class Execute : IRequest
        {
            public string Name { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Name).NotEmpty();
            }

            public class Handler : IRequestHandler<Execute>
            {
                private readonly RoleManager<IdentityRole> _roleManager;
                public Handler(RoleManager<IdentityRole> roleManager)
                {
                    _roleManager = roleManager;

                }

                public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
                {
                    var role = await _roleManager.FindByNameAsync(request.Name);
                    if(role == null)
                    {
                        throw new HandlerException(HttpStatusCode.BadRequest, new {message="Dont exist Rol"});
                    }

                    var result = await _roleManager.DeleteAsync(role);
                    if(result.Succeeded){
                        return Unit.Value;
                    }

                    throw new Exception("Dont delete rol");
                }
            }
        }
    }
}