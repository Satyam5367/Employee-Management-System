using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;

namespace EmployeeManagementSystem.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeListViewModel> GetPagedEmployeesAsync(
            string? search, string? department, string? status,
            string sortBy, string sortOrder, int page, int pageSize);

        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleStatusAsync(int id);
        Task<DashboardViewModel> GetDashboardDataAsync();
        Task<IEnumerable<string>> GetDepartmentsAsync();
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    }
}
