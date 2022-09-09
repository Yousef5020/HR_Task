namespace HR_Task.Models;

public class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public int JobRankId { get; set; }

    public int DepartmentId { get; set; }

    public DateTime EmploymentDate { get; set; }

    public JobRank JobRank { get; set; } = null!;

    public Department Department { get; set; } = null!;
}