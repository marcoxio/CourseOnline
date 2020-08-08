using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Security.Rol;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class RolController : MyControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<Unit>> CreateRol(RolNew.Execute parameters)
        {
            return await Mediator.Send(parameters);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Unit>> DeleteRol(RolDelete.Execute parameters){
            return await Mediator.Send(parameters);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<IdentityRole>>> ListRol(){
            return await Mediator.Send(new RolList.Execute());
        }

        [HttpPost("addRoleUser")]
        public async Task<ActionResult<Unit>> AddRolUser(UserRolAdd.Execute parameters)
        {
            return await Mediator.Send(parameters);
        }

        [HttpPost("deleteRolUser")]
        public async Task<ActionResult<Unit>> DeleteRolUser(UserRolDelete.Execute parameters)
        {
            return await Mediator.Send(parameters);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<List<string>>> ObtainRolByUser(string username)
        {
            return await Mediator.Send(new ObtainRoleByUser.Execute{Username = username});
        }
    }
}