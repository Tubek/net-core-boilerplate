using Microsoft.AspNetCore.Mvc;
using Nueve.Common.Enums;
using Nueve.Common.Utils;
using System.Linq;
using System.Security.Claims;

namespace Nueve.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Authenticated UserId
        /// </summary>
        public string UserId
        {
            get
            {
                var claim = HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                return claim?.Value;
            }
        }
  
        /// <summary>
        /// Authenticated user role
        /// </summary>
        public UserRole Role
        {
            get
            {
                var claim = HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
                var role = EnumUtils.ParseEnum(claim?.Value, UserRole.User);
                return role;
            }
        }
    }
}
