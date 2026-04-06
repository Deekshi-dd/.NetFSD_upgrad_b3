using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;
using WebApplication11.Services;

namespace WebApplication11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service) { _service = service; }

        [HttpGet] public IActionResult GetAll() => Ok(_service.GetAllStudents());
        [HttpGet("{id}")] public IActionResult GetById(int id) => Ok(_service.GetStudentById(id));
        [HttpPost] public IActionResult Add(Student student) { _service.AddStudent(student); return Ok(student); }
    }
}
