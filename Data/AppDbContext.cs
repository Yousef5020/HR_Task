using HR_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace HR_Task.Data;
public class AppDbContext : DbContext
{
    public AppDbContext() { }

    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Employee> Employees { get; set; } = null!;

    public DbSet<Department> Departments { get; set; } = null!;

    public DbSet<JobRank> JobRanks { get; set; } = null!;

    public DbSet<BonusType> BonusTypes { get; set; } = null!;

    public DbSet<Bonus> Bonus { get; set; } = null!;

    public DbSet<Salary> Salaries { get; set; } = null!;

#if DEBUG
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("");
        }

        base.OnConfiguring(optionsBuilder);
    }
#endif

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(e =>
        {
            e.HasKey(e => e.Id);

            e.Property(p => p.Address)
             .HasMaxLength(255);

            e.Property(p => p.PhoneNumber)
             .HasMaxLength(20);

            e.Property(p => p.Mail)
             .HasMaxLength(50);

            e.Property(p => p.FullName)
             .IsRequired()
             .HasMaxLength(255);

            e.Property(p => p.EmploymentDate)
             .IsRequired()
             .HasDefaultValue(DateTime.Now);

            e.HasOne(p => p.JobRank)
             .WithMany(r => r.Employees)
             .HasForeignKey(p => p.JobRank);

            e.HasOne(p => p.Department)
             .WithMany(d => d.Employees)
             .HasForeignKey(p => p.DepartmentId);
        });

        base.OnModelCreating(modelBuilder);
    }
}
