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

    public DbSet<AttendanceRole> AttendanceRoles { get; set; } = null!;
    
    public DbSet<Absence> Absences { get; set; } = null!;


#if DEBUG
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HR_Task;Integrated Security=True;Connect Timeout=30;");
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
             .HasForeignKey(p => p.JobRankId);

            e.HasOne(p => p.Department)
             .WithMany(d => d.Employees)
             .HasForeignKey(p => p.DepartmentId);
        });

        modelBuilder.Entity<JobRank>(e =>
        {
            e.HasKey(r => r.Id);

            e.Property(r => r.Name)
             .IsRequired()
             .HasMaxLength(50);

            e.HasMany(r => r.Employees)
             .WithOne(p => p.JobRank);

            e.HasMany(r => r.Salaries)
             .WithOne(s => s.JobRank);

            e.HasData(new JobRank[] {
                new JobRank
                {
                    Id = 1,
                    Name = "First"
                },
                new JobRank
                {
                    Id = 2,
                    Name = "Second"
                },
                new JobRank
                {
                    Id = 3,
                    Name = "Third"
                },
            });
        });

        modelBuilder.Entity<Department>(e =>
        {
            e.HasKey(d => d.Id);

            e.Property(d => d.Name)
             .IsRequired()
             .HasMaxLength(50);

            e.HasMany(d => d.Employees)
             .WithOne(p => p.Department);

            e.HasMany(d => d.Salaries)
             .WithOne(s => s.Department);
        });

        modelBuilder.Entity<Salary>(e =>
        {
            e.HasKey(s => s.Id);

            e.HasOne(s => s.JobRank)
             .WithMany(r => r.Salaries)
             .HasForeignKey(s => s.JobRankId);

            e.HasOne(s => s.Department)
             .WithMany(r => r.Salaries)
             .HasForeignKey(s => s.DepartmentId);
        });

        modelBuilder.Entity<BonusType>(e =>
        {
            e.HasKey(b => b.Id);

            e.HasMany(b => b.Bonus)
             .WithOne(b => b.BonusType);

            e.HasData(new BonusType[]
            {
                new BonusType
                {
                    Id = 1,
                    Name = "Department"
                },
                new BonusType
                {
                    Id = 2,
                    Name = "Yearly"
                },
            });
        });

        modelBuilder.Entity<Bonus>(e =>
        {
            e.HasKey(b => b.Id);

            e.HasIndex(e => new { e.TypeId, e.Role })
             .IsUnique();

            e.HasOne(b => b.BonusType)
             .WithMany(b => b.Bonus)
             .HasForeignKey(b => b.TypeId);
        });

        modelBuilder.Entity<Bonus>().Ignore(b => b.RoleDescreption);
        modelBuilder.Entity<Bonus>().Ignore(b => b.RoleDepartment);

        modelBuilder.Entity<AttendanceRole>(a =>
        {
            a.HasKey(b => b.Id);

            a.Property(a => a.MaxAbsenceDays).IsRequired();

            a.Property(a => a.MinAbsenceDays).IsRequired();

            a.Property(a => a.Rate).IsRequired();
        });

        modelBuilder.Entity<Absence>(a =>
        {
            a.HasKey(b => b.Id);

            a.HasOne(a => a.employee)
             .WithMany(e => e.Absences)
             .HasForeignKey(a => a.EmployeeId);

            a.Property(a => a.AbsenceDay).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
