using ContactRateLimitAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ContactRateLimitAPI.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    [EnableRateLimiting("fixed")]   // Apply the rate limit policy named "fixed"
    public class ContactsController : ControllerBase
    {
        // Fake data
        private static List<Contact> _contacts = new()
        {
            new Contact { ContactId = 1, Name = "Alice", Email = "alice@test.com", Phone = "1111111111" },
            new Contact { ContactId = 2, Name = "Bob",   Email = "bob@test.com",   Phone = "2222222222" },
            new Contact { ContactId = 3, Name = "Carol", Email = "carol@test.com", Phone = "3333333333" }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_contacts);
        }
    }
}
