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
        public int DepartmentId { get; set; }
        public int OfficeLocationId { get; set; }
        public int PositionId { get; set; }
        public ICollection<Employee> Employees { get; set; } = null!;
    }
}
