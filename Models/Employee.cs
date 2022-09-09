namespace HR_Task.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string FullName { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    [MaxLength(255)]
    public string Address { get; set; } = null!;

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [MaxLength(50)]
    public string Mail { get; set; } = null!;

    [Required]
    public DateTime EmploymentDate { get; set; } = DateTime.Now;

    public int JobRankId { get; set; }

    public int DepartmentId { get; set; }

    public JobRank JobRank { get; set; } = null!;

    public Department Department { get; set; } = null!;
}