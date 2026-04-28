using ContactManagement.Interfaces;
using ContactManagement.Models;

namespace ContactManagement.Services
{
    public class ContactService : IContactService
    {
        private readonly List<Contact> _contacts = new();

        public void AddContact(Contact contact)
        {
            ValidateContact(contact);

            contact.Id = GenerateId();
            _contacts.Add(contact);
        }

        public void UpdateContact(int id, Contact updatedContact)
        {
            var existing = _contacts.FirstOrDefault(c => c.Id == id);

            if (existing == null)
                throw new ArgumentException("Contact not found");

            ValidateContact(updatedContact);

            existing.Name = updatedContact.Name;
            existing.Email = updatedContact.Email;
            existing.Phone = updatedContact.Phone;
        }

        public void DeleteContact(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
                throw new ArgumentException("Contact not found");

            _contacts.Remove(contact);
        }

        public List<Contact> GetAllContacts()
        {
            return _contacts.ToList();
        }

      
        private static void ValidateContact(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(contact.Email))
                throw new ArgumentException("Email is required");

            if (string.IsNullOrWhiteSpace(contact.Phone))
                throw new ArgumentException("Phone is required");
        }

        private int GenerateId()
        {
            return _contacts.Count == 0 ? 1 : _contacts.Max(c => c.Id) + 1;
        }
    }
}
