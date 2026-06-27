using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeListViewModel> GetPagedEmployeesAsync(
            string? search, string? department, string? status,
            string sortBy, string sortOrder, int page, int pageSize)
        {
            var query = _context.Employees.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim().ToLower();
                query = query.Where(e =>
                    e.FirstName.ToLower().Contains(term) ||
                    e.LastName.ToLower().Contains(term) ||
                    e.Email.ToLower().Contains(term) ||
                    e.Department.ToLower().Contains(term) ||
                    e.JobTitle.ToLower().Contains(term));
            }

            // Filter by department
            if (!string.IsNullOrWhiteSpace(department))
                query = query.Where(e => e.Department == department);

            // Filter by status
            if (!string.IsNullOrWhiteSpace(status))
            {
                bool isActive = status == "active";
                query = query.Where(e => e.IsActive == isActive);
            }

            // Sort
            query = (sortBy?.ToLower(), sortOrder?.ToLower()) switch
            {
                ("name", "desc")       => query.OrderByDescending(e => e.FirstName),
                ("name", _)            => query.OrderBy(e => e.FirstName),
                ("department", "desc") => query.OrderByDescending(e => e.Department),
                ("department", _)      => query.OrderBy(e => e.Department),
                ("salary", "desc")     => query.OrderByDescending(e => e.Salary),
                ("salary", _)          => query.OrderBy(e => e.Salary),
                ("joining", "desc")    => query.OrderByDescending(e => e.DateOfJoining),
                ("joining", _)         => query.OrderBy(e => e.DateOfJoining),
                _                      => query.OrderBy(e => e.FirstName)
            };

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            page = Math.Max(1, Math.Min(page, Math.Max(1, totalPages)));

            var employees = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var departments = await GetDepartmentsAsync();

            return new EmployeeListViewModel
            {
                Employees = employees,
                SearchTerm = search,
                DepartmentFilter = department,
                StatusFilter = status,
                SortBy = sortBy ?? "Name",
                SortOrder = sortOrder ?? "asc",
                CurrentPage = page,
                TotalPages = totalPages,
                TotalCount = totalCount,
                PageSize = pageSize,
                Departments = departments
            };
        }

        public async Task<Employee?> GetByIdAsync(int id)
            => await _context.Employees.FindAsync(id);

        public async Task<Employee> CreateAsync(Employee employee)
        {
            employee.CreatedAt = DateTime.UtcNow;
            employee.UpdatedAt = DateTime.UtcNow;
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            employee.UpdatedAt = DateTime.UtcNow;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return false;
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return false;
            emp.IsActive = !emp.IsActive;
            emp.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var all = await _context.Employees.ToListAsync();
            return new DashboardViewModel
            {
                TotalEmployees    = all.Count,
                ActiveEmployees   = all.Count(e => e.IsActive),
                InactiveEmployees = all.Count(e => !e.IsActive),
                TotalSalaryBudget = all.Where(e => e.IsActive).Sum(e => e.Salary),
                EmployeesByDepartment = all
                    .GroupBy(e => e.Department)
                    .ToDictionary(g => g.Key, g => g.Count()),
                RecentHires = all
                    .OrderByDescending(e => e.DateOfJoining)
                    .Take(5)
                    .ToList()
            };
        }

        public async Task<IEnumerable<string>> GetDepartmentsAsync()
            => await _context.Employees
                .Select(e => e.Department)
                .Distinct()
                .OrderBy(d => d)
                .ToListAsync();

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var query = _context.Employees.Where(e => e.Email == email);
            if (excludeId.HasValue)
                query = query.Where(e => e.Id != excludeId.Value);
            return await query.AnyAsync();
        }
    }
}
