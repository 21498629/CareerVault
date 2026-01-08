using CareerVault_Backend.Models.Job;

namespace CareerVault_Backend.View_Models.Job
{
    public class JobLevelVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string DepartmentId { get; set; } = null!;
        public Department Department { get; set; } = null!;
        public string PositionId { get; set; } = null!;
        public Position Position { get; set; } = null!;
        public int JobLevelId { get; set; }
        public JobLevel JobLevel { get; set; } = null!;
        public int OfficeLocationId { get; set; }
        public OfficeLocation OfficeLocation { get; set; } = null!;
        public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();

        // OPTIONAL SALARY
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
    }
}
