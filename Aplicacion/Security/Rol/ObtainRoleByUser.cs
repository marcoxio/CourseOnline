using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using Dominio.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Security.Rol
{
    public class ObtainRoleByUser
    {
        public class Execute : IRequest<List<string>>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Execute, List<string>>
        {
                private readonly RoleManager<IdentityRole> _roleManager;
                private readonly UserManager<User> _userManager;
                public Handler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
                {
                    _userManager = userManager;
                    _roleManager = roleManager;
                }
            public async Task<List<string>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var userIden = await _userManager.FindByNameAsync(request.Username);
                if(userIden == null){
                    throw new HandlerException(HttpStatusCode.NotFound, new { message = "The user dont exists" });
                }

                var result = await _userManager.GetRolesAsync(userIden);
                return new List<string>(result); 
            }
        }
    }
}