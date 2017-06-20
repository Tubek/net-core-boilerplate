using Nueve.Common.Models;
using System.Threading.Tasks;

namespace Nueve.Common.Services
{
    public interface IUserService
    {
        Task<User> GetUser(string name, string password);

        Task<User> GetUserById(string id);

        Task CreateUser(string name, string password, string email);
    }
}
