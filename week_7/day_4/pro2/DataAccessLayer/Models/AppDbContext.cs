using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<ContactInfo> Contacts { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Your_Connection_String");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Company → Contacts (1-M)
        modelBuilder.Entity<ContactInfo>()
            .HasOne(c => c.Company)
            .WithMany(cmp => cmp.Contacts)
            .HasForeignKey(c => c.CompanyId);

        // Department → Contacts (1-M)
        modelBuilder.Entity<ContactInfo>()
            .HasOne(c => c.Department)
            .WithMany(d => d.Contacts)
            .HasForeignKey(c => c.DepartmentId);

        // Seed Data (optional)
        modelBuilder.Entity<Company>().HasData(
            new Company { CompanyId = 1, CompanyName = "ABC Ltd" },
            new Company { CompanyId = 2, CompanyName = "XYZ Corp" }
        );

        modelBuilder.Entity<Department>().HasData(
            new Department { DepartmentId = 1, DepartmentName = "HR" },
            new Department { DepartmentId = 2, DepartmentName = "IT" }
        );
    }
}
