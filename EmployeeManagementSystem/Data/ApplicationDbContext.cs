using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed departments reference data via Employee table
            builder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Email = "alice.johnson@company.com",
                    Phone = "9876543210",
                    Department = "Engineering",
                    JobTitle = "Senior Software Engineer",
                    DateOfJoining = new DateTime(2021, 3, 15),
                    Salary = 95000,
                    IsActive = true,
                    Address = "123 Tech Park, Bangalore",
                    CreatedAt = new DateTime(2021, 3, 15),
                    UpdatedAt = new DateTime(2021, 3, 15)
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Bob",
                    LastName = "Smith",
                    Email = "bob.smith@company.com",
                    Phone = "9876543211",
                    Department = "Human Resources",
                    JobTitle = "HR Manager",
                    DateOfJoining = new DateTime(2020, 6, 1),
                    Salary = 75000,
                    IsActive = true,
                    Address = "456 Corporate Blvd, Mumbai",
                    CreatedAt = new DateTime(2020, 6, 1),
                    UpdatedAt = new DateTime(2020, 6, 1)
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Carol",
                    LastName = "Davis",
                    Email = "carol.davis@company.com",
                    Phone = "9876543212",
                    Department = "Finance",
                    JobTitle = "Financial Analyst",
                    DateOfJoining = new DateTime(2022, 1, 10),
                    Salary = 70000,
                    IsActive = true,
                    Address = "789 Finance St, Delhi",
                    CreatedAt = new DateTime(2022, 1, 10),
                    UpdatedAt = new DateTime(2022, 1, 10)
                },
                new Employee
                {
                    Id = 4,
                    FirstName = "David",
                    LastName = "Wilson",
                    Email = "david.wilson@company.com",
                    Phone = "9876543213",
                    Department = "Engineering",
                    JobTitle = "Junior Developer",
                    DateOfJoining = new DateTime(2023, 8, 20),
                    Salary = 45000,
                    IsActive = false,
                    Address = "321 Startup Lane, Hyderabad",
                    CreatedAt = new DateTime(2023, 8, 20),
                    UpdatedAt = new DateTime(2023, 8, 20)
                },
                new Employee
                {
                    Id = 5,
                    FirstName = "Eva",
                    LastName = "Martinez",
                    Email = "eva.martinez@company.com",
                    Phone = "9876543214",
                    Department = "Marketing",
                    JobTitle = "Marketing Lead",
                    DateOfJoining = new DateTime(2021, 11, 5),
                    Salary = 68000,
                    IsActive = true,
                    Address = "654 Market Ave, Chennai",
                    CreatedAt = new DateTime(2021, 11, 5),
                    UpdatedAt = new DateTime(2021, 11, 5)
                }
            );
        }
    }
}
