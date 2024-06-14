using PadelBackend.Models.User;
using PadelBackend.Models.User.Dto;

namespace PadelBackend.Models.Auth.Dto
{
    public class LoginValidationDto
    {
        public bool Status { get; set; } = false;

        public string Message { get; set; } = null!;

        public User.User User { get; set; } = null!;

    }
}
