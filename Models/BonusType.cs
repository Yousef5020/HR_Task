namespace HR_Task.Models;

public class BonusType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Bonus> Bonus { get; set; } = null!;
}