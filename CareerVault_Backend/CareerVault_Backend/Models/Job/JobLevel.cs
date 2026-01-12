using System.ComponentModel.DataAnnotations;

namespace CareerVault_Backend.Models.Job
{
    public class JobLevel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<Title> Titles { get; set; }
    }
}
