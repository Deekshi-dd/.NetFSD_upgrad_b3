using ContactCachingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactCachingAPI.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _service;

        public ContactsController(ContactService service)
        {
            _service = service;
        }

        // GET /api/contacts
        [HttpGet]
        public IActionResult GetAll()
        {
            var contacts = _service.GetAll();
            return Ok(contacts);
        }

        // GET /api/contacts/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contact = _service.GetById(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }
    }
}
