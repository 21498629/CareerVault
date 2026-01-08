using System.ComponentModel.DataAnnotations;

namespace CareerVault_Backend.Models.Job
{
    public class OfficeLocation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;
        public ICollection<Title> Titles { get; set; }
    }
}
