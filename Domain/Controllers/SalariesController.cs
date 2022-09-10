using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HR_Task.Models;

namespace HR_Task.Domain.Controllers
{
    public class SalariesController : Controller
    {
        private readonly ISalaryData _salaryData;
        private readonly IJobRankData _jobRankData;
        private readonly IDepartmentData _departmentData;

        public SalariesController(ISalaryData salaryData,
            IJobRankData jobRankData,
            IDepartmentData departmentData)
        {
            _salaryData = salaryData;
            _jobRankData = jobRankData;
            _departmentData = departmentData;
        }

        public async Task<IActionResult> Index(CancellationToken token)
        {
            return View(await _salaryData.GetSalaries(token));
        }

        public async Task<IActionResult> Details(int? id, CancellationToken token)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _salaryData.GetSalary(id.Value, token);

            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        public async Task<IActionResult> Create(CancellationToken token)
        {
            ViewData["DepartmentId"] = new SelectList(await _departmentData.GetDepartments(token), "Id", "Name");
            ViewData["JobRankId"] = new SelectList(await _jobRankData.GetJobRanks(token), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,DepartmentId,JobRankId")] Salary salary, CancellationToken token)
        {
            ModelState.Remove("Department");
            ModelState.Remove("JobRank");
            if (ModelState.IsValid)
            {
                var newSalary = await _salaryData.AddSalary(salary, token);

                return RedirectToAction(nameof(Details), new { id = newSalary.Id });
            }
            ViewData["DepartmentId"] = new SelectList(await _departmentData.GetDepartments(token), "Id", "Name");
            ViewData["JobRankId"] = new SelectList(await _jobRankData.GetJobRanks(token), "Id", "Name");

            return View(salary);
        }

        public async Task<IActionResult> Edit(int? id, CancellationToken token)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _salaryData.GetSalary(id.Value, token);

            if (salary == null)
            {
                return NotFound();
            }

            ViewData["DepartmentId"] = new SelectList(await _departmentData.GetDepartments(token), "Id", "Name");
            ViewData["JobRankId"] = new SelectList(await _jobRankData.GetJobRanks(token), "Id", "Name");

            return View(salary);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Amount,DepartmentId,JobRankId")] Salary salary, CancellationToken token)
        {
            ModelState.Remove("Department");
            ModelState.Remove("JobRank");
            if (ModelState.IsValid)
            {
                await _salaryData.UpdateSalary(salary, token);
                return RedirectToAction(nameof(Details), new { id = salary.Id });
            }
            ViewData["DepartmentId"] = new SelectList(await _departmentData.GetDepartments(token), "Id", "Name");
            ViewData["JobRankId"] = new SelectList(await _jobRankData.GetJobRanks(token), "Id", "Name");

            return View(salary);
        }
    }
}
