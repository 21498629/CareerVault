using System.ComponentModel.DataAnnotations;

namespace CareerVault_Backend.Models.Job
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Title> Titles { get; set; }
    }
}
