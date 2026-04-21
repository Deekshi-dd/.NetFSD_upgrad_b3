using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication17.Models;
using WebApplication17.Repositories;

[Route("api/contacts")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactRepository _repo;

    public ContactsController(IContactRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var data = await _repo.GetAllAsync();
        return Ok(data);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var contact = await _repo.GetByIdAsync(id);
        if (contact == null)
            return NotFound();

        return Ok(contact);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(ContactInfo contact)
    {
        var created = await _repo.AddAsync(contact);
        return CreatedAtAction(nameof(GetById), new { id = created.ContactId }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, ContactInfo contact)
    {
        if (id != contact.ContactId)
            return BadRequest();

        await _repo.UpdateAsync(contact);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return Ok();
    }
}
