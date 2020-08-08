using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Interfaces;
using Dominio.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Security
{
    public class CurrentUser
    {
        public class Execute : IRequest<UserData> { }

        public class Handler : IRequestHandler<Execute, UserData>
        {
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserSession _userSession;
            public Handler(UserManager<User> userManager, IJwtGenerator jwtGenerator, IUserSession userSession)
            {
                _userSession = userSession;
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
            }

            public async Task<UserData> Handle(Execute request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userSession.ObtainUserSession());
                var resultRoles = await _userManager.GetRolesAsync(user);
                var listRoles = new List<string>(resultRoles);
                return new UserData
                {
                    FullName = user.FullName,
                    Username = user.UserName,
                    Token = _jwtGenerator.CreateToken(user,listRoles),
                    Image = null,
                    Email = user.Email
                };
            }
        }

    }
}