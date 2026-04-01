using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    [Route("calculator")]
    public class CalculatorController : Controller
    {
        // GET → Show form
        [HttpGet("add")]
        public IActionResult Add()
        {
            return View();
        }

        // POST → Handle calculation
        [HttpPost("add")]
        public IActionResult Add(int num1, int num2)
        {
            int result = num1 + num2;

            ViewData["Result"] = result;

            return View(); // same page
        }
    }

}
