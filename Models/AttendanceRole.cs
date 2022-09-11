namespace HR_Task.Models;

public class AttendanceRole
{
    public int Id { get; set; }

    public int MinAbsenceDays { get; set; }
    
    public int MaxAbsenceDays { get; set; }

    public decimal Rate { get; set; }

    public bool IsBonus { get; set; }
}
