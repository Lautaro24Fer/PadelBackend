namespace PadelBackend.Models.User.Dto
{
    public class UserValidationDataDto
    {
        public string UserNameOfEmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
