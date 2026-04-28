using ContactManagementApi.Interfaces;
using ContactManagementApi.Models;

namespace ContactManagementApi.Services
{
    public class ContactService : IContactService
    {
        private readonly List<Contact> _contacts = new();

        public List<Contact> GetAll()
        {
            return _contacts.ToList();
        }

        public Contact? GetById(int id)
        {
            return _contacts.FirstOrDefault(c => c.Id == id);
        }

        public Contact Add(Contact contact)
        {
            Validate(contact);

            contact.Id = GenerateId();
            _contacts.Add(contact);

            return contact;
        }

        public bool Update(int id, Contact contact)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            Validate(contact);

            existing.Name = contact.Name;
            existing.Email = contact.Email;
            existing.Phone = contact.Phone;

            return true;
        }

        public bool Delete(int id)
        {
            var contact = GetById(id);
            if (contact == null) return false;

            _contacts.Remove(contact);
            return true;
        }

        // Helpers
        private static void Validate(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(contact.Email))
                throw new ArgumentException("Email is required");

            if (string.IsNullOrWhiteSpace(contact.Phone))
                throw new ArgumentException("Phone is required");
        }

		private int GenerateId(){
			if (_contacts.Count == 0){
				return 1;
			}
			else{
			  int maxId = _contacts.Max(c => c.Id);
			  return maxId + 1;
			}
		}
    }
}
