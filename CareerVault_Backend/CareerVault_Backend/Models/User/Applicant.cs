using System.ComponentModel.DataAnnotations;
using CareerVault_Backend.Models.Job;
using Microsoft.AspNetCore.Builder;

namespace CareerVault_Backend.Models.User
{
    public class Applicant
    {
        public int Id { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;

        // Applicant-specific fields
        public string CVPath { get; set; } = string.Empty;
        public string Qualifications { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public int NoticePeriodInDays { get; set; }
        public string Skills { get; set; } = string.Empty;

        public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
    }
}
