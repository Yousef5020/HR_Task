using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_Task.Data;
using HR_Task.Models;

namespace HR_Task.Domain.Controllers
{
    public class AttendanceRolesController : Controller
    {
        private readonly IAttendanceRoleData _data;

        public AttendanceRolesController(IAttendanceRoleData data)
        {
            _data = data;
        }

        public async Task<IActionResult> Index(CancellationToken token)
        {
            return View(await _data.GetAttendanceRoles(token));
        }

        public async Task<IActionResult> Details(int? id, CancellationToken token)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRole = await _data.GetAttendanceRole(id.Value, token);
            if (attendanceRole == null)
            {
                return NotFound();
            }

            return View(attendanceRole);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MinAbsenceDays,MaxAbsenceDays,Rate,IsBonus")] AttendanceRole attendanceRole, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                await _data.AddAttendanceRole(attendanceRole, token);
                return RedirectToAction(nameof(Index));
            }
            return View(attendanceRole);
        }

        public async Task<IActionResult> Edit(int? id, CancellationToken token)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRole = await _data.GetAttendanceRole(id.Value, token);

            if (attendanceRole == null)
            {
                return NotFound();
            }
            return View(attendanceRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,MinAbsenceDays,MaxAbsenceDays,Rate,IsBonus")] AttendanceRole attendanceRole, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                await _data.UpdateAttendanceRoles(attendanceRole, token);
                return RedirectToAction(nameof(Index));
            }
            return View(attendanceRole);
        }
    }
}
