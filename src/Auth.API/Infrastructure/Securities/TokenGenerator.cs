using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.API.Infrastructure.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace Auth.API.Infrastructure.Securities
{
    public interface ITokenGenerator
    {
        string Generate(string userid, string name, string email);
    }
    public class TokenGenerator : ITokenGenerator
    {
        private readonly TokenConfiguration _tokenConfiguration;

        public TokenGenerator(TokenConfiguration tokenConfiguration)
        {
            _tokenConfiguration = tokenConfiguration;
        }

        public string Generate(string userid, string name, string email)
        {
            var key = Encoding.ASCII.GetBytes(_tokenConfiguration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, userid),
                    new Claim(JwtRegisteredClaimNames.UniqueName, userid),
                    new Claim(JwtRegisteredClaimNames.Sub, name),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_tokenConfiguration.ExpirationMinutes),
                SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}