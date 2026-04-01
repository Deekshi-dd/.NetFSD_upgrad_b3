using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    [Route("student")]
    public class StudentController : Controller
    {

        // get request
       
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        // post - handle form
        [HttpPost("register")]
        public IActionResult Register(string name, int age, string course)
        {
            if (string.IsNullOrWhiteSpace(name) || age == 0 || string.IsNullOrWhiteSpace(course))
            {
                ViewBag.Message = "No Data to display";
                return View("Display"); 
            }

            ViewBag.Name = name;
            ViewBag.Age = age;
            ViewBag.Course = course;

            return View("Display"); // go to Display page
        }

        // display data
        [HttpGet("display")]
        public IActionResult Display()
        {
            return View();
        }
    }
}
