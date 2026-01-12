using System.ComponentModel.DataAnnotations;
using CareerVault_Backend.Models.Job;

namespace CareerVault_Backend.View_Models.Job
{
    public class JobLevelVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<Title> Titles { get; set; }
    }
}
