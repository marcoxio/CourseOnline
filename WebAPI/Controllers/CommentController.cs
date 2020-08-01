using System;
using System.Threading.Tasks;
using Aplicacion.Comments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class CommentController : MyControllerBase
    {
       [HttpPost]
        public async Task<ActionResult<Unit>> CreateComment(New.Execute data){
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteComment(Guid id){
            return await Mediator.Send(new Delete.Execute{Id = id});
        } 
    }
}