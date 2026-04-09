using Microsoft.AspNetCore.Mvc;
using WebApplication14.Repositories;

namespace WebApplication14.Controllers
{
    public class StudentCourseController : Controller
    {

        private readonly IStudentRepository _repo;

        public StudentCourseController(IStudentRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Students()
        {
            var students = _repo.GetStudentsWithCourse();
            return View(students);
        }

        public IActionResult Courses()
        {
            var courses = _repo.GetCoursesWithStudents();
            return View(courses);
        }

    }
}
