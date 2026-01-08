using System.ComponentModel.DataAnnotations;
using CareerVault_Backend.Models.User;

namespace CareerVault_Backend.Models.Job
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }

        // FK to User
        public string AppliedByUserID { get; set; } = string.Empty;
        public AppUser AppliedByUser { get; set; } = null;

        // FK to Postion
        public string PositionId { get; set; } = string.Empty;
        public Position Position { get; set; } = null!;

        // Check application requirements against applicant qualification ???

        // APPLICATION METADATA
        public DateTime AppliedOn { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Submitted";
    }
}
