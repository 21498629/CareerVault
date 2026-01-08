using System.ComponentModel.DataAnnotations;
using CareerVault_Backend.Models.Job;
using CareerVault_Backend.Models.User;

namespace CareerVault_Backend.View_Models.Job
{
    public class TitleVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }

        // FOREIGN KEYS
        public int JobLevelId { get; set; }
        public string JobLevel { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string Department { get; set; } = null!;
        public int OfficeLocationId { get; set; }
        public string OfficeLocation { get; set; } = null!;
        public int PositionId { get; set; }
        public string Position { get; set; } = null!;
        public ICollection<Employee> Employees { get; set; } = null!;
    }
}
