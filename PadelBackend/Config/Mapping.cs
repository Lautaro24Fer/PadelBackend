using PadelBackend.Models.Racket;
using PadelBackend.Models.Racket.Dto;

namespace PadelBackend.Config
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UsersDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UserLoginResponseDto>().ReverseMap();
            CreateMap<User, Login>().ReverseMap();
            CreateMap<User, LoginValidationDto>().ReverseMap();
            CreateMap<User, UserValidationDataDto>().ReverseMap();
        }
    }
}
