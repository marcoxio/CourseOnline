using System.Threading.Tasks;
using Aplicacion.Security;
using Dominio.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UserController : MyControllerBase
    {
        //http:localhost:5000/api/User/login
        [HttpPost("login")]
        public async Task<ActionResult<UserData>> Login(Login.Execute parameters)
        {
            return await Mediator.Send(parameters);
        }
    }
}