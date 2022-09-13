using HR_Task.Models.Reporting;

namespace HR_Task.Interfaces;

public interface IReportData
{
    public Task<ICollection<EmployeesSalary>> GetEmployeesSalaries(DateTime fromDate,
            DateTime toDate,
            CancellationToken token);
}
