using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [Required, EmailAddress, StringLength(100)]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required, Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Department { get; set; } = string.Empty;

        [Required, StringLength(100)]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Date of Joining")]
        [DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Profile Picture")]
        public string? ProfilePicture { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
