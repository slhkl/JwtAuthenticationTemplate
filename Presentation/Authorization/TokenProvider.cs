using Data.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Authorization
{
    public class TokenProvider
    {
        public readonly byte[] key = Encoding.UTF8.GetBytes("generatejwttokenwiththiskeywordforthisexample");

        public readonly string issuer = "Salih";

        public readonly string audience = "Salih";

        private static TokenProvider instance;

        public static TokenProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new TokenProvider();
                return instance;
            }
        }

        public string CreateToken(LoginDto loginDto)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loginDto.Id.ToString()),
                new Claim(ClaimTypes.Name, loginDto.Name),
                new Claim(ClaimTypes.Email,loginDto.Email),
                new Claim(ClaimTypes.Role, loginDto.Role),
            };

            var securityKey = new SymmetricSecurityKey(key);

            var signIn = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer, audience, claims, DateTime.Now, DateTime.Now.AddMinutes(5), signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
