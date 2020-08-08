using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Security.Rol
{
    public class RolList
    {
        public class Execute : IRequest<List<IdentityRole>>
        {

        }

        public class Handler : IRequestHandler<Execute, List<IdentityRole>>
        {
            private readonly CoursesOnlineContext _context;
            public Handler(CoursesOnlineContext context)
            {
                _context = context;

            }
            public async Task<List<IdentityRole>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var roles = await _context.Roles.ToListAsync();
                return roles;
            }
        }
    }
}