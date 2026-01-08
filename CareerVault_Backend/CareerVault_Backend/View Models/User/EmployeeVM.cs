using CareerVault_Backend.Models.Job;
using CareerVault_Backend.Models.User;

namespace CareerVault_Backend.View_Models.User
{
    public class EmployeeProfileVM
    {
        public int Id { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;

        // EMPLOYY-SPECIFIC FIELDS
        public Title Title { get; set; } = null!;
        public decimal Salary { get; set; }
    }
}
