using System.ComponentModel.DataAnnotations;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.ViewModels
{
    // ── Auth ViewModels ──────────────────────────────────────────────
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required, StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Employee";
    }

    // ── Employee List / Search / Pagination ─────────────────────────
    public class EmployeeListViewModel
    {
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
        public string? SearchTerm { get; set; }
        public string? DepartmentFilter { get; set; }
        public string? StatusFilter { get; set; }
        public string SortBy { get; set; } = "Name";
        public string SortOrder { get; set; } = "asc";
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; } = 5;
        public IEnumerable<string> Departments { get; set; } = new List<string>();
    }

    // ── Dashboard Summary ────────────────────────────────────────────
    public class DashboardViewModel
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int InactiveEmployees { get; set; }
        public decimal TotalSalaryBudget { get; set; }
        public Dictionary<string, int> EmployeesByDepartment { get; set; } = new();
        public List<Employee> RecentHires { get; set; } = new();
    }
}
