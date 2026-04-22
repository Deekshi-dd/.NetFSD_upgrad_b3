using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactService.Models;
using ContactService.Services;
using Microsoft.AspNetCore.Authorization;

namespace ContactService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactsController(IContactService service)
        {
            _service = service;
        }

        // Only authenticated users can access this endpoint
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAll());


        // Both Admin and User roles can access
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _service.GetById(id));

        // Only Admin role can access
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(Contact contact)
        {
            await _service.Add(contact);
            return Ok();
        }

        // Only Admin role can access
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(Contact contact)
        {
            await _service.Update(contact);
            return Ok();
        }

        // Only Admin role can access
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
