using Microsoft.AspNetCore.Mvc;
using WebApplication15.DataAccess;
using WebApplication15.Models;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactRepository _repo;

    public ContactsController(IContactRepository repo)
    {
        _repo = repo;
    }

    // get - api/contacts

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contacts = await _repo.GetAllAsync();
        return Ok(contacts);
    }

    // get - api/contacts/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contact = await _repo.GetByIdAsync(id);

        if (contact == null)
            return NotFound();

        return Ok(contact);
    }

    // post - api/contacts
    [HttpPost]
    public async Task<IActionResult> Create(ContactInfo contact)
    {
        if (contact == null || string.IsNullOrEmpty(contact.FirstName))
            return BadRequest("Invalid data");

        var created = await _repo.AddAsync(contact);

        return Created($"/api/contacts/{created.ContactId}", new
        {
            message = "Contact created successfully",
            data = created
        });
    }

    // put - api/contacts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ContactInfo contact)
    {
        var updated = await _repo.UpdateAsync(id, contact);

        if (!updated)
            return NotFound();

        return Ok("Updated successfully");
    }

    // delete -  api/contacts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repo.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return Ok("Deleted successfully");
    }
}
