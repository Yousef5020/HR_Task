namespace HR_Task.Models.Reporting;

public class EmployeesSalary
{
    public string FullName { get; set; } = null!;

    public string DepartmentName { get; set; } = null!;

    public string JobRankName { get; set; } = null!;

    public decimal BasicSalary { get; set; }

    public decimal DepartmentBonus { get; set; }

    public decimal DepartmentBonusAmount
    {
        get
        {
            return BasicSalary * DepartmentBonus;
        }
    }

    public decimal YearlyBonus { get; set; }

    public decimal YearlyBonusAmount
    {
        get
        {
            return BasicSalary * YearlyBonus;
        }
    }

    public decimal AbsenceRate { get; set; }

    public decimal AbsenceRateAmount
    {
        get
        {
            return BasicSalary * AbsenceRate;
        }
    }

    public decimal NetSalary
    {
        get
        {
            return BasicSalary + DepartmentBonusAmount + YearlyBonusAmount + AbsenceRateAmount;
        }
    }
}
