using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;
using WebApplication11.Services;

namespace WebApplication11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;
        public CourseController(ICourseService service) { _service = service; }

        [HttpGet] public IActionResult GetAll() => Ok(_service.GetAllCourses());
        [HttpGet("{id}")] public IActionResult GetById(int id) => Ok(_service.GetCourseById(id));
        [HttpPost] public IActionResult Add(Course course) { _service.AddCourse(course); return Ok(course); }
    }
}
