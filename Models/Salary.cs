namespace HR_Task.Models;

public class Salary
{
    public int Id { get; set; }

    public decimal Amount { get; set; }
    
    public int DepartmentId { get; set; }

    public int JobRankId { get; set; }

    public Department Department { get; set; } = null!;

    public JobRank JobRank { get; set; } = null!;
}
