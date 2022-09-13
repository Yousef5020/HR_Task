using HR_Task.Models.Reporting;

namespace HR_Task.Services;

public class ReportData : IReportData
{
    private readonly AppDbContext _context;

    public ReportData(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<EmployeesSalary>> GetEmployeesSalaries(DateTime fromDate,
            DateTime toDate,
            CancellationToken token)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.JobRank)
            .Where(e => e.Absences.Count == 0 || e.Absences.Any(a => a.AbsenceDay >= fromDate && a.AbsenceDay <= toDate))
            .Select(employee => new EmployeesSalary
            {
                FullName = employee.FullName,
                DepartmentName = employee.Department.Name,
                JobRankName = employee.JobRank.Name,
                BasicSalary = employee.Department.Salaries
                               .Where(d => d.JobRankId == employee.JobRankId).First().Amount,
                DepartmentBonus = _context.Bonus
                               .Where(b => b.TypeId == 1 && b.Role == employee.DepartmentId)
                               .First().Rate / 100,
                YearlyBonus = _context.Bonus
                               .Where(b => b.TypeId == 2 &&
                                    DateTime.Now.AddYears(-b.Role) > employee.EmploymentDate)
                               .OrderByDescending(r => r.Role)
                               .First() == null ? 0 : _context.Bonus
                               .Where(b => b.TypeId == 2 &&
                                    DateTime.Now.AddYears(-b.Role) > employee.EmploymentDate)
                               .OrderByDescending(r => r.Role)
                               .First().Rate / 100,
                AbsenceRate = _context.AttendanceRoles
                               .Where(r => r.MaxAbsenceDays >= employee.Absences.Count &&
                                    r.MinAbsenceDays <= employee.Absences.Count)
                               .Select(r => r.IsBonus ? r.Rate : -1 * r.Rate).First() / 100
            }).ToListAsync(token);
    }
}
