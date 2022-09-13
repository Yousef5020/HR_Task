using HR_Task.Models.Reporting;
using Microsoft.AspNetCore.Mvc;

namespace HR_Task.Domain.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportData _reportData;

        public ReportsController(IReportData reportData)
        {
            _reportData = reportData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SalaryReport(DateTime? fromDate, DateTime? toDate, CancellationToken token)
        {
            if (fromDate == null || toDate == null)
            {
                return View("SalaryReport", Array.Empty<EmployeesSalary>());
            }

            return View("SalaryReport", await _reportData.GetEmployeesSalaries(fromDate.Value, toDate.Value, token));
        }
    }
}
