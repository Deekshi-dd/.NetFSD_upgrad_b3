using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;
using WebApplication7.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext _context;

    public ContactRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContactInfo>> GetAllAsync()
    {
        return await _context.Contacts
            .Include(c => c.Company)
            .Include(c => c.Department)
            .ToListAsync();
    }

    public async Task<ContactInfo> GetByIdAsync(int id)
    {
        return await _context.Contacts
            .Include(c => c.Company)
            .Include(c => c.Department)
            .FirstOrDefaultAsync(c => c.ContactId == id);
    }

    public async Task<ContactInfo> AddAsync(ContactInfo contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    public async Task UpdateAsync(ContactInfo contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}
