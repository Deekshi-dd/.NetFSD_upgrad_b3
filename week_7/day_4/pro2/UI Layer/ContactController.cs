using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[Route("contact")]
public class ContactController : Controller
{
    private readonly IContactRepository _repo;
    private readonly AppDbContext _context;

    public ContactController(IContactRepository repo, AppDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    [HttpGet("all")]
    public IActionResult ShowContacts()
    {
        var contacts = _repo.GetAllContacts();
        return View(contacts);
    }

    [HttpGet("{id}")]
    public IActionResult GetContactById(int id)
    {
        var contact = _repo.GetContactById(id);
        return View(contact);
    }

    [HttpGet("add")]
    public IActionResult AddContact()
    {
        LoadDropdowns();
        return View();
    }

    [HttpPost("add")]
    public IActionResult AddContact(ContactInfo contact)
    {
        if (ModelState.IsValid)
        {
            _repo.AddContact(contact);
            return RedirectToAction("ShowContacts");
        }
        LoadDropdowns();
        return View(contact);
    }

    [HttpGet("edit/{id}")]
    public IActionResult EditContact(int id)
    {
        var contact = _repo.GetContactById(id);
        LoadDropdowns();
        return View(contact);
    }

    [HttpPost("edit")]
    public IActionResult EditContact(ContactInfo contact)
    {
        if (ModelState.IsValid)
        {
            _repo.UpdateContact(contact);
            return RedirectToAction("ShowContacts");
        }
        LoadDropdowns();
        return View(contact);
    }

    [HttpGet("delete/{id}")]
    public IActionResult DeleteContact(int id)
    {
        _repo.DeleteContact(id);
        return RedirectToAction("ShowContacts");
    }

    private void LoadDropdowns()
    {
        ViewBag.Companies = new SelectList(_context.Companies.ToList(), "CompanyId", "CompanyName");
        ViewBag.Departments = new SelectList(_context.Departments.ToList(), "DepartmentId", "DepartmentName");
    }
}
