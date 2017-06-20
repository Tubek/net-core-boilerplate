using Microsoft.Extensions.DependencyInjection;
using Moq;
using Nueve.Common.Config;
using Nueve.Common.Enums;
using Nueve.Common.Models;
using Nueve.Common.Services;
using Nueve.Common.Telemetry;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Nueve.Test.Services
{
    public class TokenServiceTests : TestsBase
    {
        private readonly ITokenService _sut;
        private readonly string _deviceId = "deviceId";
        private readonly string _userId = "userId";

        public TokenServiceTests()
        {
            var config = Services.GetRequiredService<IAppConfig>();
            _sut = new TokenService(config);
        }

        [Fact]
        public async Task ShouldCreateTokenTest()
        {
            var testUser = new User() { Id = _userId, Role = UserRole.User };
            var token = await _sut.Create(testUser, _deviceId);

            Assert.NotNull(token.Token);
            Assert.NotNull(token.ExpiresIn);
        }


        [Fact]
        public async Task ShouldCreateRefreshTokenTest()
        {
            var testUser = new User() { Id = Guid.NewGuid().ToString(), Role = UserRole.User };
            var token = await _sut.Create(testUser, Guid.NewGuid().ToString());

            Assert.NotNull(token.Token);
            Assert.NotNull(token.ExpiresIn);
        }
    }
}
