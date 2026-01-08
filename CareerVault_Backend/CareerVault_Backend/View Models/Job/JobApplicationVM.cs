using CareerVault_Backend.Models.Job;
using CareerVault_Backend.Models.User;
using Microsoft.Identity.Client;

namespace CareerVault_Backend.View_Models
{
    public class JobApplicationVM
    {
        public int Id { get; set; }
        public int JobAvertId { get; set; }
        public JobAdvert JobAdvert { get; set; } = null!;
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; } = null!;
        public string ApplicationStatus { get; set; } = string.Empty;
        public DateTime AppliedOn { get; set; }
    }
}
