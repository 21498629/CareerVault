using CareerVault_Backend.Data;
using CareerVault_Backend.Interfaces;
using CareerVault_Backend.Models.Job;
using CareerVault_Backend.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace CareerVault_Backend.Controllers
{
    public class PositionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;
        public PositionsController(AppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET ALL POSITIONS
        [HttpGet("GetAllPositions")]
        public async Task<IActionResult> GetAllPositions()
        {
            try
            {
                var positions = await _repository.GetAllPositionsAsync();
                return Ok(positions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET VENDOR BY ID
        [HttpGet("GetPosition/{PositionID}")]
        public async Task<IActionResult> GetPosition(int PositionID)
        {
            try
            {
                var position = await _repository.GetPositionAsync(PositionID);
                if (position == null) return NotFound("Position does not exist.");
                return Ok(position);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // EDIT POSITION
        [HttpPut("EditPosition/{PositionID}")]
        public async Task<ActionResult<PositionVM>> EditPosition(int PositionId, PositionVM pvm)
        {
            try
            {
                var existingPosition = await _repository.GetPositionAsync(PositionId);

                if (existingPosition == null) return NotFound("Position does not exist.");

                existingPosition.Name = pvm.Name;

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingPosition);
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound("Your request is invalid.");
        }

        // ADD POSITION
        [HttpPost("AddPosition")]
        public async Task<IActionResult> AddPosition(PositionVM pvm)
        {
            var newPosition = new Position
            {
                Name = pvm.Name
            };

            try
            {
                _repository.Add(newPosition);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(newPosition);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound("Your request is invalid.");
        }

        // DELETE POSITION
        [HttpDelete("DeletePosition/{PositionID}")]
        public async Task<ActionResult> DeletePosition(int PositionId)
        {
            try
            {
                var existingPosition = await _repository.GetPositionAsync(PositionId);

                if (existingPosition == null) return NotFound("Position does not exist.");

                _repository.Delete(existingPosition);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok("Position deleted successfully.");
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
