using WebApplication17.Models;

namespace WebApplication17.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactInfo>> GetAllAsync();
        Task<ContactInfo> GetByIdAsync(int id);
        Task<ContactInfo> AddAsync(ContactInfo contact);
        Task UpdateAsync(ContactInfo contact);
        Task DeleteAsync(int id);
    }
}
