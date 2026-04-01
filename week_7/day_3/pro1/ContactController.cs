using Microsoft.AspNetCore.Mvc;
using WebApplication10.Models;
using WebApplication10.Services;

namespace WebApplication2.Controllers
{
    public class ContactController : Controller
    {
       private readonly IContactService _contactService;

        // constructor injection
       public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult ShowContacts()
        {
            var contacts = _contactService.GetAllContacts();
            return View(contacts);
        }

        public IActionResult GetContactById(int id)
        {
            var contact = _contactService.GetContactById(id);
            return View(contact);
        }

        // get request

        [HttpGet]
        public IActionResult AddContact()
        {
            return View();
        }

        // post request

        [HttpPost]
        public IActionResult AddContact(ContactInfo contactInfo)
        {
            _contactService.AddContact(contactInfo);
            return RedirectToAction("ShowContacts");
        }

    }
}
