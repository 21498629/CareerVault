using CareerVault_Backend.Data;
using CareerVault_Backend.Interfaces;
using CareerVault_Backend.Models.Job;
using CareerVault_Backend.View_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareerVault_Backend.Controllers
{
    public class OfficeLocationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;
        public OfficeLocationsController(AppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET ALL OFFICE LOCATIONS
        [HttpGet("GetAllOfficeLocations")]
        public async Task<IActionResult> GetAllOfficeLocations()
        {
            try
            {
                var positions = await _repository.GetAllOfficeLocationsAsync();
                return Ok(positions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET OFFICE LOCATION BY ID
        [HttpGet("GetOfficeLocation/{OfficeLocationID}")]
        public async Task<IActionResult> GetOfficeLocation(int OfficeLocationID)
        {
            try
            {
                var officeLocation = await _repository.GetOfficeLocationAsync(OfficeLocationID);
                if (officeLocation == null) return NotFound("Office Location does not exist.");
                return Ok(officeLocation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // EDIT OFFICE LOCATION
        [HttpPut("EditOfficeLocation/{OfficeLocationID}")]
        public async Task<ActionResult<OfficeLocationVM>> EditOfficeLocation(int OfficeLocationId, OfficeLocationVM lvm)
        {
            try
            {
                var existingOfficeLocation = await _repository.GetOfficeLocationAsync(OfficeLocationId);

                if (existingOfficeLocation == null) return NotFound("Office Location does not exist.");

                existingOfficeLocation.Name = lvm.Name;
                existingOfficeLocation.Address = lvm.Address;

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingOfficeLocation);
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound("Your request is invalid.");
        }

        // ADD OFFICE LOCATION
        [HttpPost("AddOfficeLocation")]
        public async Task<IActionResult> AddOfficeLocation(OfficeLocationVM lvm)
        {
            var newOfficeLocation = new OfficeLocation
            {
                Name = lvm.Name,
                Address = lvm.Address
            };

            try
            {
                _repository.Add(newOfficeLocation);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(newOfficeLocation);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound("Your request is invalid.");
        }

        // DELETE OFFICE LOCATION
        [HttpDelete("DeleteOfficeLocation/{OfficeLocationID}")]
        public async Task<ActionResult> DeleteOfficeLocation(int OfficeLocationId)
        {
            try
            {
                var existingOfficeLocation = await _repository.GetOfficeLocationAsync(OfficeLocationId);

                if (existingOfficeLocation == null) return NotFound("Office Location does not exist.");

                _repository.Delete(existingOfficeLocation);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok("Office Location deleted successfully.");
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
