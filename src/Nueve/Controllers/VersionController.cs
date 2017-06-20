using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nueve.Common.Config;
using Nueve.ViewModels.Version;

namespace Nueve.Controllers
{
    /// <summary>
    /// Version controller
    /// </summary>
    [Route("api/version")]
    public class VersionController : BaseController
    {
        private readonly IAppConfig _appConfig;

        /// <summary>
        /// Version Controller
        /// </summary>
        /// <param name="appConfig"></param>
        public VersionController(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        /// <summary>
        /// Get app name and version
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<VersionResponse> Get()
        {
            return new VersionResponse() { AppName = _appConfig.AppName, Version = _appConfig.Version };
        }
    }
}
