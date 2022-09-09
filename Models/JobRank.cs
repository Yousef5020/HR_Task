namespace HR_Task.Models;

public class JobRank{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = null!;
    
    public ICollection<Salary> Salaries { get; set; } = null!;
}

