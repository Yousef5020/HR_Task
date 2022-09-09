using HR_Task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_Task.Domain.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeData _data;

        public EmployeeController(IEmployeeData data)
        {
            _data = data ?? throw new NullReferenceException(nameof(data));
        }

        // GET: EmployeeController
        public async Task<ActionResult> Index(CancellationToken token)
        {
            return View(await _data.GetEmployees(token));
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(int id, CancellationToken token)
        {
            return View(await _data.GetEmployee(id, token));
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind] Employee employee, CancellationToken token)
        {
            try
            {
                var newEmployee = await _data.AddEmployee(employee, token);
                return RedirectToAction(nameof(Details), newEmployee.Id);
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
