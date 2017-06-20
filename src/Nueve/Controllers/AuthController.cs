using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nueve.Common.Services;
using System.Security;
using System.Web.Http;
using System.Net;
using Nueve.ViewModels.Auth;

namespace Nueve.Controllers
{
    /// <summary>
    /// Auth controller
    /// </summary>
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        /// <summary>
        /// AuthController
        /// </summary>
        /// <param name="tokenService"></param>
        /// <param name="userService"></param>
        public AuthController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        /// <summary>
        /// Obtain token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="SecurityException"></exception>
        [HttpPost]
        [Route("")]
        public async Task<AuthResponse> Post([FromBody] AuthRequest request)
        {
            var user = await _userService.GetUser(request.Email, request.Password);

            if (user != null)
            {
                var token = await _tokenService.Create(user, "api-user");

                return new AuthResponse
                {
                    Token = $"Bearer {token.Token}",
                    ExpiresIn = token.ExpiresIn,
                };
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}
