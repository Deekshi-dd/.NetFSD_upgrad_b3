using WebApplication15.Models;

namespace WebApplication15.DataAccess
{
    public class ContactRepository : IContactRepository
    {
        // Static List (MANDATORY)
        private static List<ContactInfo> contacts = new List<ContactInfo>
{
    new ContactInfo
    {
        ContactId = 1, FirstName = "Aaysha", LastName = "Khan", EmailId = "aaysha@gmail.com",
        MobileNo = 9876543210, Designation = "Developer", CompanyId = 1, DepartmentId = 1
    },
    new ContactInfo
    {
        ContactId = 2, FirstName = "Rahul", LastName = "Sharma", EmailId = "rahul@gmail.com",
        MobileNo = 9123456780, Designation = "Tester", CompanyId = 1, DepartmentId = 2
    },
    new ContactInfo
    {
        ContactId = 3, FirstName = "Priya", LastName = "Verma", EmailId = "priya@gmail.com",
        MobileNo = 9988776655, Designation = "Manager", CompanyId = 2, DepartmentId = 3
    }
};

        public async Task<IEnumerable<ContactInfo>> GetAllAsync()
        {
            return await Task.FromResult(contacts);
        }

        public async Task<ContactInfo> GetByIdAsync(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.ContactId == id);
            return await Task.FromResult(contact);
        }

        public async Task<ContactInfo> AddAsync(ContactInfo contact)
        {
            contacts.Add(contact);
            return await Task.FromResult(contact);
        }

        public async Task<bool> UpdateAsync(int id, ContactInfo updatedContact)
        {
            var oldContact = contacts.FirstOrDefault(c => c.ContactId == id);

            if (oldContact == null)
                return await Task.FromResult(false);

            oldContact.FirstName = updatedContact.FirstName;
            oldContact.LastName = updatedContact.LastName;
            oldContact.EmailId = updatedContact.EmailId;
            oldContact.MobileNo = updatedContact.MobileNo;
            oldContact.Designation = updatedContact.Designation;
            oldContact.CompanyId = updatedContact.CompanyId;
            oldContact.DepartmentId = updatedContact.DepartmentId;

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.ContactId == id);

            if (contact == null)
                return await Task.FromResult(false);

            contacts.Remove(contact);
            return await Task.FromResult(true);
        }
    }
}
