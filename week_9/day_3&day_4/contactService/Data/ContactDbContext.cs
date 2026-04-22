using Microsoft.EntityFrameworkCore;
using ContactService.Models;

namespace ContactService.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
            : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
