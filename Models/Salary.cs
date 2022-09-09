namespace HR_Task.Models;

public class Salary
{
    public int Id { get; set; }

    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public int DepartmentId { get; set; }

    [Required]
    public int JobRankId { get; set; }

    public Department Department { get; set; } = null!;

    public JobRank JobRank { get; set; } = null!;
}
