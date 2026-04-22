using ContactService.Models;

namespace ContactService.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAll();
        Task<Contact> GetById(int id);
        Task Add(Contact contact);
        Task Update(Contact contact);
        Task Delete(int id);
    }
}
