using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Nueve.Test.Integration.Controllers
{
    public class AuthControllerTests : TestsBase
    {
        [Fact]
        public async Task Should_Fail_When_Passing_Invalid_Model()
        {
            var token = await AuthorizeUser(string.Empty, string.Empty, string.Empty);
            Assert.Equal(HttpStatusCode.BadRequest, token.StatusCode);

            token = await AuthorizeUser(Guid.NewGuid().ToString(), string.Empty, string.Empty);
            Assert.Equal(HttpStatusCode.BadRequest, token.StatusCode);

            token = await AuthorizeUser(string.Empty, Guid.NewGuid().ToString(), string.Empty);
            Assert.Equal(HttpStatusCode.BadRequest, token.StatusCode);
        }
    }
}
