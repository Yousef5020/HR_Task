using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HR_Task.Models;

namespace HR_Task.Domain.Controllers
{
    public class BonusesController : Controller
    {
        private readonly IBonusData _bonusData;
        private readonly IDepartmentData _departmentData;

        public BonusesController(IBonusData bonus,
            IDepartmentData departmentData)
        {
            _bonusData = bonus;
            _departmentData = departmentData;
        }

        public async Task<IActionResult> Index(CancellationToken token)
        {
            return View(await _bonusData.GetBonuses(token));
        }

        public async Task<IActionResult> Details(int? id, CancellationToken token)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bonus = await _bonusData.GetBonus(id.Value, token);

            if (bonus == null)
            {
                return NotFound();
            }

            return View(bonus);
        }

        public async Task<IActionResult> Create(CancellationToken token)
        {
            ViewData["TypeId"] = new SelectList(await _bonusData.GetBonusTypes(token), "Id", "Name");
            ViewData["Role"] = new SelectList(await _departmentData.GetDepartments(token), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeId,Rate,Role,RoleDepartment")] Bonus bonus, CancellationToken token)
        {
            ModelState.Remove("RoleDescreption");
            ModelState.Remove("BonusType");

            if (ModelState.IsValid)
            {
                await _bonusData.AddBonus(bonus, token);
                return RedirectToAction(nameof(Details), new { id = bonus.Id });
            }
            ViewData["TypeId"] = new SelectList(await _bonusData.GetBonusTypes(token), "Id", "Name");
            ViewData["Role"] = new SelectList(await _departmentData.GetDepartments(token), "Id", "Name");
            return View(bonus);
        }

        // GET: Bonuses/Edit/5
        public async Task<IActionResult> Edit(int? id, CancellationToken token)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bonus = await _bonusData.GetBonus(id.Value, token);

            if (bonus == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(await _bonusData.GetBonusTypes(token), "Id", "Name");
            ViewData["Role"] = new SelectList(await _departmentData.GetDepartments(token), "Id", "Name");
            return View(bonus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,TypeId,Rate,Role")] Bonus bonus, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                var updatedBonus = await _bonusData.UpdateBonus(bonus, token);

                if (updatedBonus == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Details), new { id = updatedBonus.Id });
            }

            ViewData["TypeId"] = new SelectList(await _bonusData.GetBonusTypes(token), "Id", "Name");
            ViewData["Role"] = new SelectList(await _departmentData.GetDepartments(token), "Id", "Name");
            return View(bonus);
        }
    }
}
