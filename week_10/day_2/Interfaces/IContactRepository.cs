using ContactManagementApi.Models;

namespace ContactManagementApi.Interfaces
{
    public interface IContactService
    {
        List<Contact> GetAll();
        Contact? GetById(int id);
        Contact Add(Contact contact);
        bool Update(int id, Contact contact);
        bool Delete(int id);
    }
}
