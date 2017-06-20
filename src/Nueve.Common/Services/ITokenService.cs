using Nueve.Common.Models;
using System.Threading.Tasks;
using Nueve.Common.Security;

namespace Nueve.Common.Services
{
    public interface ITokenService
    {
        Task<AuthToken> Create(User user, string deviceId);
    }
}
