using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR_Task.Data;
using HR_Task.Models;
using NuGet.Common;

namespace HR_Task.Domain.Controllers
{
    public class AbsencesController : Controller
    {
        private readonly IAbsenceData _absenceData;
        private readonly IEmployeeData _employeeData;

        public AbsencesController(IAbsenceData absenceData,
            IEmployeeData employeeData)
        {
            _absenceData = absenceData;
            _employeeData = employeeData;
        }

        public async Task<IActionResult> Index(CancellationToken token)
        {
            return View(await _absenceData.GetAbsences(token));
        }

        public async Task<IActionResult> Details(int? id, CancellationToken token)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _absenceData.GetAbsence(id.Value, token);

            if (absence == null)
            {
                return NotFound();
            }

            return View(absence);
        }

        public async Task<IActionResult> Create(CancellationToken token)
        {
            ViewData["EmployeeId"] = new SelectList(await _employeeData.GetEmployees(token), "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,AbsenceDay")] Absence absence, CancellationToken token)
        {
            ModelState.Remove("employee");

            if (ModelState.IsValid)
            {
                await _absenceData.AddAbsence(absence, token);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(await _employeeData.GetEmployees(token), "Id", "FullName");
            return View(absence);
        }

        // GET: Absences/Edit/5
        public async Task<IActionResult> Edit(int? id, CancellationToken token)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _absenceData.GetAbsence(id.Value, token);
            if (absence == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(await _employeeData.GetEmployees(token), "Id", "FullName");
            return View(absence);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,EmployeeId,AbsenceDay")] Absence absence, CancellationToken token)
        {
            ModelState.Remove("employee");

            if (ModelState.IsValid)
            {
                var updatedU = await _absenceData.UpdateAbsences(absence, token);

                if (updatedU == null) return NotFound();

                return RedirectToAction(nameof(Details), new { id = updatedU.Id });
            }
            ViewData["EmployeeId"] = new SelectList(await _employeeData.GetEmployees(token), "Id", "FullName");
            return View(absence);
        }

    }
}
