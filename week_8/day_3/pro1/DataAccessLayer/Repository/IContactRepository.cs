using WebApplication15.Models;

namespace WebApplication15.DataAccess
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactInfo>> GetAllAsync();
        Task<ContactInfo> GetByIdAsync(int id);
        Task<ContactInfo> AddAsync(ContactInfo contact);
        Task<bool> UpdateAsync(int id, ContactInfo contact);
        Task<bool> DeleteAsync(int id);
    }
}
