using CareerVault_Backend.Data;
using CareerVault_Backend.Interfaces;
using CareerVault_Backend.Models.Job;
using CareerVault_Backend.View_Models;
using CareerVault_Backend.View_Models.Job;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerVault_Backend.Controllers
{
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;
        public DepartmentsController(AppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET ALL DEPARTMENTS
        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var department = await _repository.GetAllDepartmentsAsync();
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET DEPARTMENT BY ID
        [HttpGet("GetDepartment/{DepartmentID}")]
        public async Task<IActionResult> GetDepartment(int DepartmentID)
        {
            try
            {
                var department = await _repository.GetDepartmentAsync(DepartmentID);
                if (department == null) return NotFound("Department does not exist.");
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // EDIT DEPARTMENT
        [HttpPut("EditDepartment/{DepartmentID}")]
        public async Task<ActionResult<DepartmentVM>> EditDepartment(int DepartmentId, DepartmentVM dvm)
        {
            try
            {
                var existingDepartment = await _repository.GetDepartmentAsync(DepartmentId);

                if (existingDepartment == null) return NotFound("Department does not exist.");

                existingDepartment.Name = dvm.Name;

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingDepartment);
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound("Your request is invalid.");
        }

        // ADD DEPARTMENT
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment(DepartmentVM dvm)
        {
            var newDepartment = new Department
            {
                Name = dvm.Name
            };

            try
            {
                _repository.Add(newDepartment);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(newDepartment);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound("Your request is invalid.");
        }

        // DELETE DEPARTMENT
        [HttpDelete("DeleteDepartment/{DepartmentID}")]
        public async Task<ActionResult> DeleteDepartment(int DepartmentId)
        {
            try
            {
                var existingDepartment = await _repository.GetDepartmentAsync(DepartmentId);

                if (existingDepartment == null) return NotFound("Department does not exist.");

                _repository.Delete(existingDepartment);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok("Department deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound("Your request is invalid.");
        }
    }
}
