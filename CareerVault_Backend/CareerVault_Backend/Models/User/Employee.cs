using System.ComponentModel.DataAnnotations;
using CareerVault_Backend.Models.Job;

namespace CareerVault_Backend.Models.User
{
    public class Employee
    {
        public int Id { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;

        // EMPLOYY-SPECIFIC FIELDS
        public Title Title { get; set; } = null!;
        public decimal Salary { get; set; }
    }
}
