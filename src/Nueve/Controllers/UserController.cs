using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nueve.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Nueve.Common.Models;
using Nueve.ViewModels.User;

namespace Nueve.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get authorized user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<User> Get()
        {
            var user = new User()
            {
                Role = Role,
                Id = UserId
            };

            return user;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task Post([FromBody] UserCreateRequest request)
        {
            await _userService.CreateUser(request.Name, request.Password, request.Email);
        }
    }
}
