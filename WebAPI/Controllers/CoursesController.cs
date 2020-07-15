using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Courses;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetAllCourses()
        {
            return await _mediator.Send(new Consult.ListCourses());
        }
        

        
        [HttpGet("{id}")]

        public async Task<ActionResult<Course>> DetailCourse(int id)
        {
            return await _mediator.Send(new ConsultId.UniqueCourse{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateCourse(New.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> EditCourse(int id, Edit.Execute data)
        {
            data.CourseId = id;
            return await _mediator.Send(data);
        }
    }
}