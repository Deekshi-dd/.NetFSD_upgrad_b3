using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{

    [Route("feedback")]
    public class FeedbackController : Controller
    {
        
        [HttpGet("form")]
        public IActionResult Form()
        {
            return View();
        }

      
        [HttpPost("form")]
        public IActionResult Form(string name, string comments, int rating)
        {
            // validation 
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(comments) || rating < 1)
            {
                ViewData["Message"] = "Please fill all fields correctly!";
                return View();
            }

            if (rating >= 4)
            {
                ViewData["Message"] = "Thank You for your feedback!";
            }
            else
            {
                ViewData["Message"] = "We will improve based on your feedback.";
            }

            return View(); 
        }
    }
}
