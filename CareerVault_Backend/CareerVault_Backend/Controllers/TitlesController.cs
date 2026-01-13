using CareerVault_Backend.Data;
using CareerVault_Backend.Interfaces;
using CareerVault_Backend.Models.Job;
using CareerVault_Backend.View_Models.Job;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareerVault_Backend.Controllers
{
    public class TitleController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;

        public TitleController(AppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET ALL TITLES
        [HttpGet("GetAllTitles")]
        public async Task<IActionResult> GetAllTitles()
        {
            try
            {
                var titles = await _context.Titles
                .Include(t => t.Department)
                .Include(t => t.JobLevel)
                .Include(t => t.Position)
                .Include(t => t.OfficeLocation)
                .Select(t => new TitleVM
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    DepartmentId = t.DepartmentId,
                    JobLevelId = t.JobLevelId,
                    PositionId = t.PositionId,
                    OfficeLocationId = t.OfficeLocationId,
                    MinSalary = t.MinSalary,
                    MaxSalary = t.MaxSalary
                })
                .ToListAsync();
                return Ok(titles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET TITLE BY ID
        [HttpGet("GetTitle/{TitleID}")]
        public async Task<ActionResult> GetTitle(int TitleID)
        {
            try
            {
                var title = await _repository.GetTitleAsync(TitleID);
                if (title == null) return NotFound("Title does not exist.");
                return Ok(title);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // ADD TITLE
        [HttpPost]
        [Route("AddTitle")]
        /*public async Task<IActionResult> AddTitle(TitleVM tvm)
        {
            var title = new Title
            {
                Name = tvm.Name,
                Description = tvm.Description,
                MinSalary = tvm.MinSalary,
                MaxSalary = tvm.MaxSalary,
                DepartmentId = tvm.DepartmentId,
                JobLevelId = tvm.JobLevelId,
                PositionId = tvm.PositionId,
                OfficeLocationId = tvm.OfficeLocationId
            };

            try
            {
                _repository.Add(title);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }

            return Ok(title);
        }*/

        public async Task<IActionResult> AddTitle([FromBody] TitleVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Optional FK validation (recommended)
            /*if (!await _context.JobLevels.AnyAsync(j => j.Id == model.JobLevelId))
                return BadRequest("Invalid JobLevel");

            if (!await _context.Departments.AnyAsync(d => d.Id == model.DepartmentId))
                return BadRequest("Invalid Department");

            if (!await _context.OfficeLocations.AnyAsync(o => o.Id == model.OfficeLocationId))
                return BadRequest("Invalid Office Location");

            if (!await _context.Positions.AnyAsync(p => p.Id == model.PositionId))
                return BadRequest("Invalid Position");*/

            var title = new Title
            {
                Name = model.Name,
                Description = model.Description,
                MinSalary = model.MinSalary,
                MaxSalary = model.MaxSalary,
                JobLevelId = model.JobLevelId,
                DepartmentId = model.DepartmentId,
                OfficeLocationId = model.OfficeLocationId,
                PositionId = model.PositionId
            };

            _context.Titles.Add(title);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Title created successfully", title.Id });
        }


        // EDIT TITLE
        [HttpPut]
        [Route("EditTitle/{TitleID}")]
        public async Task<ActionResult<TitleVM>> EditTitle(int TitleID, TitleVM tvm)
        {
            try
            {
                var existingTitle = await _repository.GetDepartmentAsync(TitleID);

                if (existingTitle == null) return NotFound($"The department does not exist");

                existingTitle.Id = tvm.Id;
                existingTitle.Name = tvm.Name;


                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingTitle);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Your request is invalid");
        }

        // DELETE TITLE
        [HttpDelete]
        [Route("DeleteTitle/{TitleID}")]
        public async Task<IActionResult> DeleteTitle(int TitleID)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var existingTitle = await _repository.GetTitleAsync(TitleID);

                if (existingTitle == null) return NotFound($"The department does not exist");

                _repository.Delete(existingTitle);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingTitle);
                }
                ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Your request is invalid");
        }
    }
}
