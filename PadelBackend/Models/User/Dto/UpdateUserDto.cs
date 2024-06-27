using System.ComponentModel.DataAnnotations;

namespace PadelBackend.Models.User.Dto
{
    public class UpdateUserDto
    {
        [MaxLength(100)]
        public string? Name { get; set; } 
        [MaxLength(20)]
        public string? UserName { get; set; } 
        [EmailAddress]
        public string? Email { get; set; } 
        [MinLength(8)]
        public string? Password { get; set; }
    }
}
