using Microsoft.EntityFrameworkCore;
using CategoryService.Models;

namespace CategoryService.Data
{
    public class CategoryDbContext : DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
