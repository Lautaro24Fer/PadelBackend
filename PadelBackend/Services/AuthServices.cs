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
        public Task<LoginValidationDto> ValidateCredentials(Login login);
        public bool IsValidAddress(string input);
        public bool IsValidUserNameFormat(string input);
    }
    public class AuthServices : IAuthServices
    {
        private string secretKey;
        private readonly IUsersServices usersServices;
        private readonly IMapper mapper;
        private readonly IEncoderService encoderService;
        public AuthServices(IConfiguration config, IUsersServices usersServices, IMapper mapper, IEncoderService encoderService)
        {
            secretKey = config.GetSection("jwtSettings").GetSection("secretKey").ToString() ?? null!;
            this.usersServices = usersServices;
            this.mapper = mapper;
            this.encoderService = encoderService;
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
        public async Task<LoginValidationDto> ValidateCredentials(Login login)
        {
            
            var user = await usersServices.GetOneUserByUserNameOrEmail(login.UsernameOrMailAddress);
            if (!user.Status)
            {
                throw new Exception(user.Message);
            }
            if (!encoderService.Verify(login.Password, user.User.Password))
            {
                user.Status = false;
                user.Message = "The credentials do not match";
                return user;
            }
            return user;
        }

        public bool IsValidAddress(string input)
        {
            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(input);
        }

        public bool IsValidUserNameFormat(string input)
        {
            string usernamePattern = @"^[a-z0-9_]+$";
            return Regex.IsMatch(input, usernamePattern);
        }
    }
}
