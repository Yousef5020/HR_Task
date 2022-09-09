using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HR_Task.Models;

namespace HR_Task.Domain.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeData _employeeData;
        private readonly IDepartmentData _departmentData;
        private readonly IJobRankData _jobRankData;

        public EmployeesController(
            IEmployeeData emplyeeData,
            IDepartmentData departmentData,
            IJobRankData jobRankData)
        {
            _employeeData = emplyeeData ?? throw new NullReferenceException(nameof(emplyeeData));
            _departmentData = departmentData ?? throw new NullReferenceException(nameof(departmentData));
            _jobRankData = jobRankData ?? throw new NullReferenceException(nameof(jobRankData));
        }

        public async Task<IActionResult> Index(CancellationToken cancellation)
        {
            return View(await _employeeData.GetEmployees(cancellation));
        }

        public async Task<IActionResult> Details(int? id, CancellationToken cancellation)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeData.GetEmployee(id.Value, cancellation);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<IActionResult> Create(CancellationToken cancellation)
        {
            ViewData["DepartmentId"] = new SelectList(await _departmentData.GetDepartments(cancellation), "Id", "Name");
            ViewData["JobRankId"] = new SelectList(await _jobRankData.GetJobRanks(cancellation), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,BirthDate,Address,PhoneNumber,Mail,EmploymentDate,JobRankId,DepartmentId")] Employee employee, CancellationToken cancellation)
        {
            ModelState.Remove("Department");
            ModelState.Remove("JobRank");

            if (ModelState.IsValid)
            {
                var newEmployee = await _employeeData.AddEmployee(employee, cancellation);
                return RedirectToAction(nameof(Details), new { id = newEmployee.Id });
            }
            ViewData["DepartmentId"] = new SelectList(await _departmentData.GetDepartments(cancellation), "Id", "Name", employee.DepartmentId);
            ViewData["JobRankId"] = new SelectList(await _jobRankData.GetJobRanks(cancellation), "Id", "Name", employee.JobRankId);
            return View(employee);
        }

        public async Task<IActionResult> Edit(int? id, CancellationToken cancellation)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeData.GetEmployee(id.Value, cancellation);
            if (employee == null)
            {
                return NotFound();
            }

            ViewData["DepartmentId"] = new SelectList(await _departmentData.GetDepartments(cancellation), "Id", "Name", employee.DepartmentId);
            ViewData["JobRankId"] = new SelectList(await _jobRankData.GetJobRanks(cancellation), "Id", "Name", employee.JobRankId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,FullName,BirthDate,Address,PhoneNumber,Mail,EmploymentDate,JobRankId,DepartmentId")] Employee employee, CancellationToken cancellation)
        {
            ModelState.Remove("Department");
            ModelState.Remove("JobRank");

            if (ModelState.IsValid)
            {
                await _employeeData.UpdateEmployees(employee, cancellation);
                return RedirectToAction(nameof(Details),new { id = employee.Id });
            }
            ViewData["DepartmentId"] = new SelectList(await _departmentData.GetDepartments(cancellation), "Id", "Name", employee.DepartmentId);
            ViewData["JobRankId"] = new SelectList(await _jobRankData.GetJobRanks(cancellation), "Id", "Name", employee.JobRankId);
            return View(employee);
        }

        public async Task<IActionResult> Delete(int? id, CancellationToken cancellation)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeData.GetEmployee(id.Value, cancellation);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellation)
        {
            await _employeeData.DeleteEmployee(id, cancellation);

            return RedirectToAction(nameof(Index));
        }
    }
}
