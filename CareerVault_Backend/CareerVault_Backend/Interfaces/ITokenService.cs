using CareerVault_Backend.Models.User;

namespace CareerVault_Backend.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(AppUser user, IEnumerable<string> roles);
    }
}
