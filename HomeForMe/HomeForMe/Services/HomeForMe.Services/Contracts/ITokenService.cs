using HomeForMe.Data.Models;
using System.Threading.Tasks;

namespace HomeForMe.Services.Contracts
{
    public interface ITokenService
    {
        Task<string> GenerateToken(AppUser user);
    }
}
