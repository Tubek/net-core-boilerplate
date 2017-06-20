using Moq;
using Nueve.Common.Enums;
using Nueve.Common.Models;
using Nueve.Common.Services;
using Nueve.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Nueve.ViewModels.Auth;
using Xunit;

namespace Nueve.Test.Controllers
{
    public class AuthControllerTests : TestsBase
    {
        private readonly string _testUserName;
        private readonly string _testUserPassword;
        private readonly string _testUserId;

        private readonly AuthController _sut;
        public AuthControllerTests()
        {
            _testUserName = Guid.NewGuid().ToString();
            _testUserPassword = Guid.NewGuid().ToString();
            _testUserId = Guid.NewGuid().ToString();

            var testUserRole = UserRole.SuperUser;

            var tokenServiceMock = new Mock<ITokenService>();
            var userServiceMock = new Mock<IUserService>();

            userServiceMock
                .Setup(
                    usm => usm.GetUser(_testUserName, _testUserPassword))
                .Returns(Task.FromResult(new User()
                {
                    Id = _testUserId,
                    Name = _testUserName,
                    Role = testUserRole
                }));

            userServiceMock
                .Setup(
                    usm => usm.GetUserById(It.IsAny<string>()))
                .Returns(Task.FromResult(new User()
                {
                    Id = _testUserId,
                    Name = _testUserName,
                    Role = testUserRole
                }));

            tokenServiceMock
                .Setup(tsm => tsm.Create(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new AuthToken() {Token = Guid.NewGuid().ToString(), ExpiresIn = 4}));

            _sut = new AuthController(tokenServiceMock.Object, userServiceMock.Object);
        }

        [Fact]
        public async Task Post_Should_Fail_For_Non_Existing_User()
        {
            try
            {
                var request = new AuthRequest()
                {
                    Email = Guid.NewGuid().ToString(),
                    Password = Guid.NewGuid().ToString(),
                };
                await _sut.Post(request);
            }
            catch (Exception e)
            {
                Assert.IsType<HttpResponseException>(e);
            }
        }

        [Fact]
        public async Task Post_Should_Return_Token_For_Existing_User()
        {
            var request = new AuthRequest()
            {
                Email = _testUserName,
                Password = _testUserPassword,
            };

            var result = await _sut.Post(request);

            Assert.NotEmpty(result.Token);
            Assert.NotEqual(0, result.ExpiresIn);
        }
    }
}
