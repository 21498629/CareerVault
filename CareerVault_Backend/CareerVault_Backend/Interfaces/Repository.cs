using CareerVault_Backend.Data;
using CareerVault_Backend.Models.Job;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using CareerVault_Backend.Models;
using CareerVault_Backend.Models.User;
using CareerVault_Backend.Models.Job;
using Microsoft.EntityFrameworkCore;
using CareerVault_Backend.Data;

namespace CareerVault_Backend.Interfaces
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        // USER
        public async Task<AppUser[]> GetAllUsersAsync()
        {
            IQueryable<AppUser> query = _context.Users;
            return await query.ToArrayAsync();
        }

        public async Task<AppUser> GetUserAsync(string userID)
        {
            IQueryable<AppUser> query = _context.Users;
            return await query.FirstOrDefaultAsync();
        }

        // EMPLOYEE PROFILE
        public async Task<Employee[]> GetAllEmployeeProfilesAsync()
        {
            IQueryable<Employee> query = _context.EmployeeProfiles;
            return await query.ToArrayAsync();
        }

        public async Task<Employee> GetEmployeeProfileAsync(int employeeProfileID)
        {
            IQueryable<Employee> query = _context.EmployeeProfiles;
            return await query.FirstOrDefaultAsync();
        }

        // APPLICANT PROFILE
        public async Task<Applicant[]> GetAllApplicantProfilesAsync()
        {
            IQueryable<Applicant> query = _context.Applicant;
            return await query.ToArrayAsync();
        }

        public async Task<Applicant> GetApplicantProfileAsync(int applicantID)
        {
            IQueryable<Applicant> query = _context.Applicant;
            return await query.FirstOrDefaultAsync();
        }

        // DEPARTMENT
        public async Task<Department[]> GetAllDepartmentsAsync()
        {
            IQueryable<Department> query = _context.Departments;
            return await query.ToArrayAsync();
        }

        public async Task<Department> GetDepartmentAsync(int departmentID)
        {
            IQueryable<Department> query = _context.Departments;
            return await query.FirstOrDefaultAsync();
        }

        // LEVEL
        public async Task<JobLevel[]> GetAllLevelsAsync()
        {
            IQueryable<JobLevel> query = _context.JobLevels;
            return await query.ToArrayAsync();
        }

        public async Task<JobLevel> GetLevelAsync(int levelID)
        {
            IQueryable<JobLevel> query = _context.JobLevels;
            return await query.FirstOrDefaultAsync();
        }

        // TITLE
        public async Task<Title[]> GetAllTitlesAsync()
        {
            IQueryable<Title> query = _context.Titles;
            return await query.ToArrayAsync();
        }

        public async Task<Title> GetTitleAsync(int titleID)
        {
            IQueryable<Title> query = _context.Titles;
            return await query.FirstOrDefaultAsync();
        }

        // APPLICATION
        public async Task<JobApplication[]> GetAllApplicationsAsync()
        {
            IQueryable<JobApplication> query = _context.JobApplications;
            return await query.ToArrayAsync();
        }

        public async Task<JobApplication> GetApplicationAsync(int applicationID)
        {
            IQueryable<JobApplication> query = _context.JobApplications;
            return await query.FirstOrDefaultAsync();
        }

        // OFFICE LOCATION
        public async Task<OfficeLocation[]> GetAllOfficeLocationsAsync()
        {
            IQueryable<OfficeLocation> query = _context.OfficeLocations;
            return await query.ToArrayAsync();
        }

        public async Task<OfficeLocation> GetOfficeLocationAsync(int locationID)
        {
            IQueryable<OfficeLocation> query = _context.OfficeLocations;
            return await query.FirstOrDefaultAsync();
        }

        // POSITION
        public async Task<Position[]> GetAllPositionsAsync()
        {
            IQueryable<Position> query = _context.Positions;
            return await query.ToArrayAsync();
        }

        public async Task<Position> GetPositionAsync(int positionID)
        {
            IQueryable<Position> query = _context.Positions;
            return await query.FirstOrDefaultAsync();
        }

        // JOB ADVERT
        public async Task<JobAdvert[]> GetAllAdvertsAsync()
        {
            IQueryable<JobAdvert> query = _context.JobAdverts;
            return await query.ToArrayAsync();
        }

        public async Task<JobAdvert> GetAdvertAsync(int advertID)
        {
            IQueryable<JobAdvert> query = _context.JobAdverts;
            return await query.FirstOrDefaultAsync();
        }
    }
}
