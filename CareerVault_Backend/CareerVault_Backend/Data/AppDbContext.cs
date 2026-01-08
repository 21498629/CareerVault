using CareerVault_Backend.Models.User;
using CareerVault_Backend.Models.Job;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;


namespace CareerVault_Backend.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Applicant> Applicant { get; set; }

        public DbSet<Employee> EmployeeProfiles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobAdvert> JobAdverts { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobLevel> JobLevels { get; set; }
        public DbSet<OfficeLocation> OfficeLocations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Title> Titles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobApplication>(entity =>
            {
                // Whatever application stuff
            });


        }
    }
}
