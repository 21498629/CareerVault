using CareerVault_Backend.Data;
using CareerVault_Backend.Interfaces;
using CareerVault_Backend.Models.Job;
using CareerVault_Backend.View_Models;
using CareerVault_Backend.View_Models.Job;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerVault_Backend.Controllers
{
    public class JobLevelsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;
        public JobLevelsController(AppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET ALL JOBLEVELS
        [HttpGet("GetAllJobLevels")]
        public async Task<IActionResult> GetAllJobLevels()
        {
            try
            {
                var jobLevel = await _repository.GetAllJobLevelsAsync();
                return Ok(jobLevel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET JOBLEVEL BY ID
        [HttpGet("GetJobLevel/{JobLevelID}")]
        public async Task<IActionResult> GetJobLevel(int JobLevelID)
        {
            try
            {
                var jobLevel = await _repository.GetJobLevelAsync(JobLevelID);
                if (jobLevel == null) return NotFound("JobLevel does not exist.");
                return Ok(jobLevel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // EDIT JOBLEVEL
        [HttpPut("EditJobLevel/{JobLevelID}")]
        public async Task<ActionResult<JobLevelVM>> EditJobLevel(int JobLevelId, JobLevelVM jlvm)
        {
            try
            {
                var existingJobLevel = await _repository.GetJobLevelAsync(JobLevelId);

                if (existingJobLevel == null) return NotFound("JobLevel does not exist.");

                existingJobLevel.Name = jlvm.Name;

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingJobLevel);
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound("Your request is invalid.");
        }

        // ADD JOBLEVEL
        [HttpPost("AddJobLevel")]
        public async Task<IActionResult> AddJobLevel(JobLevelVM jlvm)
        {
            var newJobLevel = new JobLevel
            {
                Name = jlvm.Name
            };

            try
            {
                _repository.Add(newJobLevel);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(newJobLevel);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound("Your request is invalid.");
        }

        // DELETE JOBLEVEL
        [HttpDelete("DeleteJobLevel/{JobLevelID}")]
        public async Task<ActionResult> DeleteJobLevel(int JobLevelId)
        {
            try
            {
                var existingJobLevel = await _repository.GetJobLevelAsync(JobLevelId);

                if (existingJobLevel == null) return NotFound("JobLevel does not exist.");

                _repository.Delete(existingJobLevel);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok("JobLevel deleted successfully.");
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
