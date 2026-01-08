using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CareerVault_Backend.Models.User;
using CareerVault_Backend.View_Models.User;
using CareerVault_Backend.Data;

namespace CareerVault_Backend.Controllers
{
    //[Authorize]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("GetAllRoles")]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles
                .Select(r => new { r.Id, r.Name })
                .ToList();

            return Ok(roles);
        }

        // CREATE ROLE
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Role name is required.");

            if (await _roleManager.RoleExistsAsync(roleName))
                return BadRequest("Role already exists.");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Role created successfully." });
        }

        //GET ROLE
        [HttpGet("GetRole")]
        public async Task<IActionResult> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound("Role not found.");

            return Ok(new { role.Id, role.Name });
        }

        //UPDATE ROLE
        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] string newRoleName)
        {
            if (string.IsNullOrWhiteSpace(newRoleName))
                return BadRequest("Role name is required.");

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound("Role not found.");

            role.Name = newRoleName;
            role.NormalizedName = newRoleName.ToUpper();

            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Role updated successfully." });
        }

        // DELETE ROLE
        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound("Role not found.");

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Role deleted successfully." });
        }

        // USERS PER ROLE
        [HttpPost("GetUsersInRole")]
        public async Task<IActionResult> GetUsersPerRole()
        {
            var roles = _roleManager.Roles.ToList();

            var result = new List<object>();

            foreach (var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

                result.Add(new
                {
                    ID = role.Id,
                    Name = role.Name,
                    UserCount = usersInRole.Count
                });
            }

            return Ok(result);
        }
    }

}
