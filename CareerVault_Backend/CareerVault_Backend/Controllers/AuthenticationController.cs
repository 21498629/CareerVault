using CareerVault_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CareerVault_Backend.View_Models.User;
using CareerVault_Backend.Models.User;
using Microsoft.AspNetCore.Authorization;
using CareerVault_Backend.Data;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace CareerVault_Backend.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _appDbContext;

        public AuthenticationController(IUserRepository userRepository, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, SignInManager<AppUser> signInManager, AppDbContext appDbContext)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
        }

        // API ENDPOINTS PROTECTION BASED ON ROLES

        // Only Admins can call this
        [HttpGet("dashboard")]
        //[Authorize(Roles = "Admin")]                     
        public IActionResult GetDashboard()
        {
            return Ok("Welcome to the secret Admin dashboard!");
        }

        // Admin OR Manager can call this
        [HttpGet("reports")]
        //[Authorize(Roles = "Admin,Manager")]             
        public IActionResult GetReports()
        {
            return Ok("Here are your reports");
        }

        // You can also put it on the whole controller
        [Authorize(Roles = "Admin")]                     // ← Applies to all actions below
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(AppUserVM uvm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrEmpty(uvm.Password))
                    return BadRequest(new { message = "Password is required" });

                var user = new AppUser
                {
                    UserName = uvm.Email,
                    Email = uvm.Email,
                    Name = uvm.Name,
                    Surname = uvm.Surname,
                    Address = uvm.Address,
                    PhoneNumber = uvm.PhoneNumber,
                    DateOfBirth = uvm.DateOfBirth,
                    CreatedAt = uvm.CreatedAt ?? DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, uvm.Password);
                if (!result.Succeeded)
                    return StatusCode(500, result.Errors);

                // Assign default "User" role to new registrations
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (!roleResult.Succeeded)
                    return StatusCode(500, roleResult.Errors);

                var roles = await _userManager.GetRolesAsync(user);
                var token = _tokenService.CreateToken(user, roles);

                return Ok(new
                {
                    message = "User created successfully",
                    roles

                });
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
        // --------------------------------------------

        // REGISTER
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AppUserVM uvm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrEmpty(uvm.Password))
                    return BadRequest(new { message = "Password is required" });

                var user = new AppUser
                {
                    UserName = uvm.Email,
                    Email = uvm.Email,
                    Name = uvm.Name,
                    Surname = uvm.Surname,
                    Address = uvm.Address,
                    PhoneNumber = uvm.PhoneNumber,
                    DateOfBirth = uvm.DateOfBirth,
                    CreatedAt = uvm.CreatedAt ?? DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, uvm.Password);
                if (!result.Succeeded)
                    return StatusCode(500, result.Errors);

                // Assign default "User" role to new registrations
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (!roleResult.Succeeded)
                    return StatusCode(500, roleResult.Errors);

                var roles = await _userManager.GetRolesAsync(user);
                var token = _tokenService.CreateToken(user, roles);

                return Ok(new
                {
                    message = "User registered successfully",
                    token,
                    roles

                });
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }

        }

        // LOGIN
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM lvm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userManager.FindByEmailAsync(lvm.Email.ToLower());

                if (user == null)
                {
                    return Unauthorized("Invalid email or password");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, lvm.Password, false);

                var roles = await _userManager.GetRolesAsync(user);

                /*var scopes = roles.Contains("Admin")
                ? new[] { "api.read", "api.write" }
                : new[] { "api.read" };*/

                var token = _tokenService.CreateToken(user, roles /*, scopes*/);

                if (!result.Succeeded)
                {
                    return Unauthorized("Email Address not found and/or Password incorrect.");
                }

                return Ok(new
                {
                    token,
                    roles,
                    //scopes
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET USER BY EMAIL
        [HttpGet("GetUser/{Email}")]
        public async Task<ActionResult> GetUserByEmail(string Email)
        {
            var users = await _userRepository.GetUserByEmailAsync(Email);
            return Ok(users);
        }

        // GET ALL USERS
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        // EDIT USER
        [HttpPut("EditUser/{Email}")]
        public async Task<IActionResult> EditUser(string Email, AppUserVM uvm)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user == null)
                    return NotFound(new { message = "User not found." });

                user.Email = uvm.Email;
                user.Name = uvm.Name;
                user.Surname = uvm.Surname;
                user.Address = uvm.Address;
                user.PhoneNumber = uvm.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) return BadRequest(result.Errors);

                // UPDATE ROLE
                if (!string.IsNullOrEmpty(uvm.Role))
                {
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);

                    if (!await _roleManager.RoleExistsAsync(uvm.Role))
                        return BadRequest("Role does not exist.");

                    await _userManager.AddToRoleAsync(user, uvm.Role);
                }

                return Ok(new { user, message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }

        // DELETE USER
        [HttpDelete("DeleteUser/{Email}")]
        public async Task<IActionResult> DeleteUser(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null) NotFound("User not found.");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "User deleted successfully." });
        }
    }
}
