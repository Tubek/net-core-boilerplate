using System;
using System.Threading.Tasks;
using Nueve.Common.Models;
using Nueve.Common.Enums;

namespace Nueve.Common.Services
{
    public class UserService : IUserService
    {
        /// <summary>
        /// Get user by name and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> GetUser(string email, string password)
        {
            if (email == "email" && password == "password")
            {
                var user = new User()
                {
                    Id = Guid.NewGuid().ToString("n"),
                    Name = email,
                    Role = UserRole.User
                };

                return user;
            }

            // Credentials are invalid, or account doesn't exist
            return null;
        }

        public Task<User> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public Task CreateUser(string name, string password, string email)
        {
            throw new NotImplementedException();
        }
    }
}
