using System.Threading.Tasks;
using Xunit;

namespace Nueve.Test.Integration.Security
{
    public class TokenTests : TestsBase
    {
        [Fact]
        public async Task CanGetToken()
        {
            var token = await AuthorizeUser("username", "password", "deviceId");
            Assert.NotNull(token);
        }
    }
}
