using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppUILayer.Controllers
{


    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _repo;

        public ContactController(IContactRepository repo)
        {
            _repo = repo;
        }

        // Show All Contacts
        [HttpGet("list")]
        public IActionResult ShowContacts()
        {
            var contacts = _repo.GetAllContacts();
            return View(contacts);
        }

        // Add Contact (GET)
        [HttpGet("add")]
        public IActionResult AddContact()
        {
            ViewBag.Companies = _repo.GetCompanies();
            ViewBag.Departments = _repo.GetDepartments();
            return View();
        }

        // Add Contact (POST)
        [HttpPost("add")]
        public IActionResult AddContact(ContactInfo contact)
        {
            if (ModelState.IsValid)
            {
                _repo.AddContact(contact);
                return RedirectToAction("ShowContacts");
            }

            // reload dropdown if validation fails
            ViewBag.Companies = _repo.GetCompanies();
            ViewBag.Departments = _repo.GetDepartments();
            return View(contact);
        }

        // Edit Contact (GET)
        [HttpGet]
        public IActionResult EditContact(int id)
        {
            var contact = _repo.GetContactById(id);

            ViewBag.Companies = _repo.GetCompanies();
            ViewBag.Departments = _repo.GetDepartments();

            return View(contact);
        }

        // Edit Contact (POST)
        [HttpPost]
        public IActionResult EditContact(ContactInfo contact)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateContact(contact);
                return RedirectToAction("ShowContacts");
            }

            ViewBag.Companies = _repo.GetCompanies();
            ViewBag.Departments = _repo.GetDepartments();

            return View(contact);
        }

        // Delete Contact
        [HttpGet("delete/{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contact = _repo.GetContactById(id);
            return View(contact);
        }

        [HttpPost("delete-confirm/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteContact(id);
            return RedirectToAction("ShowContacts");
        }

        // Get Contact By Id 
        [HttpGet("details/{id}")]
        public IActionResult GetContactById(int id)
        {
            var contact = _repo.GetContactById(id);
            return View(contact);
        }
    }
}
