using ContactManagement.Interfaces;
using ContactManagement.Models;
using ContactManagement.Services;

IContactService service = new ContactService();

service.AddContact(new Contact
{
    Name = "shree",
    Email = "shree@gmail.com",
    Phone = "1234679944"
});

service.AddContact(new Contact
{
    Name = "raw",
    Email = "raw@gmail.com",
    Phone = "8787853210"
});

var contacts = service.GetAllContacts();

foreach (var contact in contacts)
{
    Console.WriteLine($"{contact.Id}: {contact.Name} - {contact.Email} - {contact.Phone}");
}
