using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Nueve.Common.Models;
using Microsoft.IdentityModel.Tokens;
using static Nueve.Common.Security.TokenProvider;
using Nueve.Common.Config;
using Nueve.Common.Security;

namespace Nueve.Common.Services
{
    public class TokenService : ITokenService
    {
        private readonly IAppConfig _config;

        public TokenService(IAppConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Create token for user 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task<AuthToken> Create(User user, string deviceId)
        {
            var signingOptions = GetOptions();

            var identity = new ClaimsIdentity(new System.Security.Principal.GenericIdentity(user.Id, "Token"), new Claim[] { });
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var jwt = new JwtSecurityToken(
                issuer: signingOptions.Issuer,
                audience: signingOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(signingOptions.Expiration),
                signingCredentials: signingOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthToken() { Token = encodedJwt, ExpiresIn = (int)signingOptions.Expiration.TotalSeconds };
        }

        /// <summary>
        /// Get token provider options
        /// </summary>
        /// <returns></returns>
        private TokenProviderOptions GetOptions()
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.IdentitySecret));

            var options = new TokenProviderOptions
            {
                Audience = _config.IdentityAudience,
                Issuer = _config.IdentityAudience,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Expiration = TimeSpan.FromHours(_config.IdentityTokenLiftimeInHours)
            };

            return options;
        }
    }
}
