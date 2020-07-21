using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Dominio.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Security
{
    public class Login
    {
        public class Execute : IRequest<UserData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute, UserData>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;
            public Handler(UserManager<User> userManager,
            SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
                _signInManager = signInManager;

            }

        public async Task<UserData> Handle(Execute request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new HandlerException(HttpStatusCode.Unauthorized);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                return new UserData
                {
                    FullName = user.FullName,
                    Token = _jwtGenerator.CreateToken(user),
                    Username = user.UserName,
                    Email = user.Email,
                    Image = null
                };
            }

            throw new HandlerException(HttpStatusCode.Unauthorized);
        }
    }
}
}