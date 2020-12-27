using HomeForMe.Data.Models;

namespace HomeForMe.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}
