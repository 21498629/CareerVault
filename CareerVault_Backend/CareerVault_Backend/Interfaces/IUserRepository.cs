using Microsoft.AspNetCore.Identity;
using CareerVault_Backend.Models.User;
using System.Threading.Tasks;

namespace CareerVault_Backend.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUserAsync(AppUser user, string password);
        Task<SignInResult> LoginUserAsync(string email, string password, bool rememberMe);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<IList<AppUser>> GetAllUsersAsync();
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
        Task<IList<string>> GetUserRolesAsync(AppUser user);
        Task<bool> RoleExistsAsync(string roleName);

    }
}
