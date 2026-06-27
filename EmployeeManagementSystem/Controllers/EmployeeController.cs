using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: /Employee
        public async Task<IActionResult> Index(
            string? search, string? department, string? status,
            string sortBy = "Name", string sortOrder = "asc",
            int page = 1, int pageSize = 5)
        {
            var vm = await _employeeService.GetPagedEmployeesAsync(
                search, department, status, sortBy, sortOrder, page, pageSize);
            return View(vm);
        }

        // GET: /Employee/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var emp = await _employeeService.GetByIdAsync(id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        // GET: /Employee/Create
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _employeeService.GetDepartmentsAsync();
            return View();
        }

        // POST: /Employee/Create
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (await _employeeService.EmailExistsAsync(employee.Email))
                ModelState.AddModelError("Email", "This email is already registered.");

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _employeeService.GetDepartmentsAsync();
                return View(employee);
            }

            await _employeeService.CreateAsync(employee);
            TempData["Success"] = $"Employee {employee.FullName} created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Employee/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _employeeService.GetByIdAsync(id);
            if (emp == null) return NotFound();
            ViewBag.Departments = await _employeeService.GetDepartmentsAsync();
            return View(emp);
        }

        // POST: /Employee/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();

            if (await _employeeService.EmailExistsAsync(employee.Email, id))
                ModelState.AddModelError("Email", "This email is already used by another employee.");

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _employeeService.GetDepartmentsAsync();
                return View(employee);
            }

            await _employeeService.UpdateAsync(employee);
            TempData["Success"] = $"Employee {employee.FullName} updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Employee/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _employeeService.GetByIdAsync(id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emp = await _employeeService.GetByIdAsync(id);
            var name = emp?.FullName ?? "Employee";
            await _employeeService.DeleteAsync(id);
            TempData["Success"] = $"{name} has been deleted.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Employee/ToggleStatus/5
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            await _employeeService.ToggleStatusAsync(id);
            TempData["Success"] = "Employee status updated.";
            return RedirectToAction(nameof(Index));
        }
    }
}
