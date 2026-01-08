using CareerVault_Backend.Models.Job;
using Microsoft.AspNetCore.Builder;
using CareerVault_Backend.Models;
using CareerVault_Backend.Models.Job;
using CareerVault_Backend.Models.User;

namespace CareerVault_Backend.Interfaces
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // USER
        Task<AppUser[]> GetAllUsersAsync();
        Task<AppUser> GetUserAsync(string userID);

        // EMPLOYEE PROFILE
        Task<Employee[]> GetAllEmployeeProfilesAsync();
        Task<Employee> GetEmployeeProfileAsync(int employeeProfileID);

        // APPLICANT PROFILE
        Task<Applicant[]> GetAllApplicantProfilesAsync();
        Task<Applicant> GetApplicantProfileAsync(int applicantProfileID);

        // DEPARTMENT
        Task<Department[]> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentAsync(int departmentID);

        // LEVEL
        Task<JobLevel[]> GetAllLevelsAsync();
        Task<JobLevel> GetLevelAsync(int levelID);

        // TITLE
        Task<Title[]> GetAllTitlesAsync();
        Task<Title> GetTitleAsync(int titleID);

        // APPLICATION
        Task<JobApplication[]> GetAllApplicationsAsync();
        Task<JobApplication> GetApplicationAsync(int applicationID);

        // OFFICE LOCATION
        Task<OfficeLocation[]> GetAllOfficeLocationsAsync();
        Task<OfficeLocation> GetOfficeLocationAsync(int locationID);

        // POSITION
        Task<Position[]> GetAllPositionsAsync();
        Task<Position> GetPositionAsync(int positionID);

        // JOB ADVERT
        Task<JobAdvert[]> GetAllAdvertsAsync();
        Task<JobAdvert> GetAdvertAsync(int advertID);

    }

}
