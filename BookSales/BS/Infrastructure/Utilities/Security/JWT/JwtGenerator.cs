using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Utilities.Security.JWT
{
    public class JwtGenerator
    {
        private readonly IConfiguration _configuration;
        private TokenOptions _tokenOptions;
        private DateTime _expirationDate;

        public JwtGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        public AccessToken GenerateAccessToken()
        {
            _expirationDate = DateTime.Now.AddMinutes(_tokenOptions.Expiration);

            var sKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var sCredentials = new SigningCredentials(sKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken
              (
                 issuer: _tokenOptions.Issuer,
                 audience: _tokenOptions.Audience,
                 expires: _expirationDate,
                 notBefore: DateTime.Now,
                 claims: new List<Claim> { new Claim("KEY1", "VALUE1"), new Claim("KEY2", "VALUE2"), new Claim("KEY3", "VALUE3") },
                 signingCredentials: sCredentials
              );


            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.WriteToken(securityToken);

            return new AccessToken()
            {
                Token = token,
                ExpirationDate = _expirationDate,
                Claims = null
            };
        }
    }
}
