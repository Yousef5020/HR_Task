namespace HR_Task.Models;

public class Absence
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public DateTime AbsenceDay { get; set; }

    public Employee employee { get; set; } = null!;
}
