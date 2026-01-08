using System.ComponentModel.DataAnnotations;
using CareerVault_Backend.Models.User;

namespace CareerVault_Backend.Models.Job
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //public ICollection<Title> Tiles { get; set; } = new List<Employee>();
    }
}
