using System.ComponentModel.DataAnnotations;

namespace PadelBackend.Models.Auth
{
    public class Login
    {
        [Required]
        public string UsernameOrMailAddress { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
