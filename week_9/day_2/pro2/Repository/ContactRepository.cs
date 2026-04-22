using ContactPagingAPI.Data;
using ContactPagingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactPagingAPI.Repositories
{
    public class ContactRepository
    {
        private readonly AppDbContext _db;

        public ContactRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<(List<Contact> Contacts, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            int totalCount = await _db.Contacts.CountAsync();

            // Skip: jump over previous pages. Take: grab only this page's records
            var contacts = await _db.Contacts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (contacts, totalCount);
        }
    }
