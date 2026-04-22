using ContactService.Models;
using ContactService.Repositories;

namespace ContactService.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repo;

        public ContactService(IContactRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Contact>> GetAll() => _repo.GetAll();
        public Task<Contact> GetById(int id) => _repo.GetById(id);
        public Task Add(Contact contact) => _repo.Add(contact);
        public Task Update(Contact contact) => _repo.Update(contact);
        public Task Delete(int id) => _repo.Delete(id);
    }
}
