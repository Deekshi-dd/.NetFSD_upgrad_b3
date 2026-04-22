using ContactService.Models;

namespace ContactService.Services
{
    public interface IContactService
    {
        Task<List<Contact>> GetAll();
        Task<Contact> GetById(int id);
        Task Add(Contact contact);
        Task Update(Contact contact);
        Task Delete(int id);
    }
}
