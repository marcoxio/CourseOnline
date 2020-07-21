using System;
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
    public class Register
    {
        public class Execute : IRequest<UserData>
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
        }

         public class ExecuteValidator : AbstractValidator<Execute>{
            public ExecuteValidator(){
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
            private readonly IJwtGenerator _jwtGenerator;
            public Handler(CoursesOnlineContext context, UserManager<User> userManager, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
                _context = context;

            }

            public async Task<UserData> Handle(Execute request, CancellationToken cancellationToken)
            {
                var exist = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if(exist)
                {
                    throw new HandlerException(HttpStatusCode.BadRequest, new {message ="This email already exists"});
                }

                var existUserName = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();
                if(existUserName){
                    throw new HandlerException(HttpStatusCode.BadRequest, new {mensaje = "this username already exists"});
                    
                }

                   var user = new User{
                    FullName = request.FullName,
                    Email = request.Email,
                    UserName = request.Username
                };

                var result = await _userManager.CreateAsync(user,request.Password);
                if(result.Succeeded){
                    return new UserData{
                        FullName = user.FullName,
                        Token = _jwtGenerator.CreateToken(user),
                        Username = user.UserName,
                        Email = user.Email
                    };
                }

                throw new Exception("dont add new user");
            }
        }
    }
}