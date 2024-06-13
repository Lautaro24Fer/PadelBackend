using Microsoft.IdentityModel.Tokens;
using PadelBackend.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PadelBackend.Services
{
    public class AuthServices
    {
        private string secretKey;
        public AuthServices(IConfiguration config)
        {
            secretKey = config.GetSection("jwtSettings").GetSection("secretKey").ToString() ?? null!;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("id", user.Id.ToString()));
            claims.AddClaim(new Claim("username", user.UserName.ToString()));
            // En primera instancia no usaremos roles de usuario
            //if (user.Roles != null)
            //{
            //    foreach (var role in user.Roles)
            //    {
            //        claims.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            //    }
            //}
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(tokenConfig);
            return token;
        }
    }
}
