using System.ComponentModel.DataAnnotations;
using CareerVault_Backend.Models.User;

namespace CareerVault_Backend.Models.Job
{
    public class Title
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }

        // FOREIGN KEYS
        public int JobLevelId { get; set; }
        public JobLevel JobLevel { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int OfficeLocationId { get; set; }
        public OfficeLocation OfficeLocation { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
