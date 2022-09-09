using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_Task.Data;
using HR_Task.Models;

namespace HR_Task.Domain.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentData _departmentData;

        public DepartmentsController(IDepartmentData departmentData)
        {
            _departmentData = departmentData;
        }

        public async Task<IActionResult> Index(CancellationToken token)
        {
            return View(await _departmentData.GetDepartments(token));
        }

        public async Task<IActionResult> Details(int? id, CancellationToken token)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _departmentData.GetDepartment(id.Value, token);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Department department, CancellationToken token)
        {
            ModelState.Remove("Salaries");
            ModelState.Remove("Employees");

            if (ModelState.IsValid)
            {
                var newDepartment = await _departmentData.AddDepartment(department, token);
                return RedirectToAction(nameof(Details), new { id = newDepartment.Id });
            }
            return View(department);
        }

        public async Task<IActionResult> Edit(int? id, CancellationToken token)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _departmentData.GetDepartment(id.Value, token);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name")] Department department, CancellationToken token)
        {
            ModelState.Remove("Salaries");
            ModelState.Remove("Employees");

            if (ModelState.IsValid)
            {
                var updatedDepartment = await _departmentData.UpdateDepartments(department, token);

                if (updatedDepartment == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Details), new { id = updatedDepartment.Id });
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id, CancellationToken token)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _departmentData.GetDepartment(id.Value, token);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken token)
        {
            await _departmentData.DeleteDepartment(id, token);
            return RedirectToAction(nameof(Index));
        }
    }
}
