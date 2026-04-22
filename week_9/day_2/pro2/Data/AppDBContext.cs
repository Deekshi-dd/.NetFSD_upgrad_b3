using ContactPagingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactPagingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
