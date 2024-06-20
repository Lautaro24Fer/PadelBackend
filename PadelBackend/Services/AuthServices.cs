using Microsoft.IdentityModel.Tokens;
using PadelBackend.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using PadelBackend.Models.Auth;
using AutoMapper;
using PadelBackend.Models.User.Dto;
using PadelBackend.Models.Auth.Dto;


namespace PadelBackend.Services
{
    public interface IAuthServices
    {
        public string GenerateJwtToken(User user);
    }
    public class AuthServices : IAuthServices
    {
        private readonly string secretKey;
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
