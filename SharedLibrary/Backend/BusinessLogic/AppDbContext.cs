using Microsoft.EntityFrameworkCore;
using SharedLibrary.Backend.DataAccess;

namespace SharedLibrary.Backend.BusinessLogic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<TimeRegistration> TimeRegistrations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=/Users/kristinacortsen/RiderProjects/registration-system/SharedLibrary/database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>()
            .HasMany(d => d.Cases)
            .WithOne(c => c.Department)
            .HasForeignKey(c => c.DepartmentID);

        modelBuilder.Entity<Department>()
            .HasMany(d => d.Employees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentID);

        modelBuilder.Entity<Case>()
            .HasMany(c => c.TimeRegistrations)
            .WithOne(tr => tr.Case)
            .HasForeignKey(tr => tr.CaseID);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.TimeRegistrations)
            .WithOne(tr => tr.Employee)
            .HasForeignKey(tr => tr.EmployeeID);


        modelBuilder.Entity<Department>().HasData(
            new Department { DepartmentID = 4, Name = "IT", Number = 12 },
            new Department { DepartmentID = 5, Name = "HR", Number = 13 },
            new Department { DepartmentID = 6, Name = "Support", Number = 14 }
        );
    }
}