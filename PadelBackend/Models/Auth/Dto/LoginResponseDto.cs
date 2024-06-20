using PadelBackend.Models.User.Dto;

namespace PadelBackend.Models.Auth.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = null!;

        public UserLoginResponseDto User { get; set; } = null!;
    }
}
