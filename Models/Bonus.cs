namespace HR_Task.Models;

public class Bonus
{
    public int Id { get; set; }

    public int TypeId { get; set; }

    public decimal Rate { get; set; }

    public int Role { get; set; }

    public int RoleDepartment { get; set; }

    public string RoleDescreption { get; set; } = null!;

    public BonusType BonusType { get; set; } = null!;
}
