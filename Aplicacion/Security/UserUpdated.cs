using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Dominio.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Security
{
    public class UserUpdated
    {
        public class Execute : IRequest<UserData>
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
        }

        public class ExecuteValidator : AbstractValidator<Execute>
        {
            public ExecuteValidator()
            {
                RuleFor(x => x.FullName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute, UserData>
        {
            private readonly CoursesOnlineContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtHandler;
            private IPasswordHasher<User> _passwordHasher;

            public Handler(CoursesOnlineContext context, UserManager<User> userManager, IJwtGenerator jwtHandler, IPasswordHasher<User> passwordHasher)
            {
                _context = context;
                _userManager = userManager;
                _jwtHandler = jwtHandler;
                _passwordHasher = passwordHasher;
            }
            public async Task<UserData> Handle(Execute request, CancellationToken cancellationToken)
            {
                var userIden = await _userManager.FindByNameAsync(request.Username);
                if (userIden == null)
                {
                    throw new HandlerException(HttpStatusCode.NotFound, new { Message = "Dont exist user with username" });
                }

                var result = await _context.Users.Where(x => x.Email == request.Email && x.UserName != request.Username).AnyAsync();
                if (result)
                {
                    throw new HandlerException(HttpStatusCode.InternalServerError, new { mensaje = "This email belong other user" });
                }

                userIden.FullName = request.FullName;
                userIden.PasswordHash = _passwordHasher.HashPassword(userIden, request.Password);
                userIden.Email = request.Email;

                var resultUpdate = await _userManager.UpdateAsync(userIden);
                var resultRoles = await _userManager.GetRolesAsync(userIden);
                var listRoles = new List<string>(resultRoles);

                if (resultUpdate.Succeeded)
                {
                    return new UserData
                    {
                        FullName = userIden.FullName,
                        Username = userIden.UserName,
                        Email = userIden.Email,
                        Token = _jwtHandler.CreateToken(userIden, listRoles)
                    };
                }

                throw new Exception("Dont update user");
            }
        }
    }
}