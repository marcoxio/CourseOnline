using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Instructors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConnection.Instructor;

namespace WebAPI.Controllers
{
    public class InstructorController : MyControllerBase
    {
        [HttpGet]
         public async Task<ActionResult<List<InstructorModel>>> ObtenerInstructores(){
            return await Mediator.Send(new Consult.ListInstructor());
        }

          
        [HttpGet("{id}")]

        public async Task<ActionResult<InstructorModel>> DetailInstructor(Guid id)
        {
            return await Mediator.Send(new ConsultId.Execute{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateInstructor(New.Execute data){
            return await Mediator.Send(data);
        }

         [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateInstructor(Guid id, Edit.Execute data){
            data.InstructorId = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteInstructor(Guid id)
        {
            return await Mediator.Send(new Delete.Execute{Id = id});
        }
    }
}