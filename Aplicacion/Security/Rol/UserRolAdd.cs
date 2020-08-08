using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using Dominio.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Security.Rol
{
    public class UserRolAdd
    {
        public class Execute : IRequest
        {
            public string Username { get; set; }
            public string RolName { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.RolName).NotEmpty();
            }
         }

            public class Handler : IRequestHandler<Execute>
            {
                private readonly RoleManager<IdentityRole> _roleManager;
                private readonly UserManager<User> _userManager;
                public Handler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
                {
                    _userManager = userManager;
                    _roleManager = roleManager;
                }

                public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
                {
                    var role = await _roleManager.FindByNameAsync(request.RolName);
                    if (role == null)
                    {
                        throw new HandlerException(HttpStatusCode.NotFound, new { message = "The rol dont exist" });
                    }

                    var userIden = await _userManager.FindByNameAsync(request.Username);
                    if(userIden == null){
                        throw new HandlerException(HttpStatusCode.NotFound, new { message = "The user dont exists" });
                    }

                    var result = await _userManager.AddToRoleAsync(userIden, request.RolName);
                    if (result.Succeeded)
                    {
                        return Unit.Value;
                    }

                    throw new Exception("Dont add Role at user");   
                }
            }
       
    }
}