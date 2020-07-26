using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Courses;
using Dominio.DTOs;
using Dominio.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    // [Authorize]
    public class CoursesController : MyControllerBase
    {
      
        [HttpGet]
        public async Task<ActionResult<List<CourseDto>>> GetAllCourses()
        {
            return await Mediator.Send(new Consult.ListCourses());
        }
        

        
        [HttpGet("{id}")]

        public async Task<ActionResult<CourseDto>> DetailCourse(Guid id)
        {
            return await Mediator.Send(new ConsultId.UniqueCourse{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateCourse(New.Execute data)
        {
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> EditCourse(Guid id, Edit.Execute data)
        {
            data.CourseId = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteCourse(Guid id)
        {
            return await Mediator.Send(new Delete.Execute{Id = id});
        }
    }
}