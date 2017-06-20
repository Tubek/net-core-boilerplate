using System;
using Microsoft.IdentityModel.Tokens;

namespace Nueve.Common.Security
{
    public class TokenProvider
    {
        public class TokenProviderOptions
        {
            public string Issuer { get; set; }

            public string Audience { get; set; }

            public TimeSpan Expiration { get; set; } = TimeSpan.FromHours(1);

            public SigningCredentials SigningCredentials { get; set; }
        }
    }
}
