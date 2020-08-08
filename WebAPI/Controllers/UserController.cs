using System.Threading.Tasks;
using Aplicacion.Security;
using Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UserController : MyControllerBase
    {
        [AllowAnonymous]
        //http:localhost:5000/api/User/login
        [HttpPost("login")]
        public async Task<ActionResult<UserData>> Login(Login.Execute parameters)
        {
            return await Mediator.Send(parameters);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserData>> Register(Register.Execute parameters)
        {
            return await Mediator.Send(parameters);
        }

        [HttpGet]
        public async Task<ActionResult<UserData>> ReturnUser(){
            return await Mediator.Send(new CurrentUser.Execute());
        }

        [HttpPut]
        public async Task<ActionResult<UserData>> UpdateUser(UserUpdated.Execute parameters){
            return await Mediator.Send(parameters);
        }
    }
}