using AutoMapper;
using PadelBackend.Models.User.Dto;
using PadelBackend.Models.User;
using PadelBackend.Models.Auth;
using PadelBackend.Models.Auth.Dto;

namespace PadelBackend.Config
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UsersDto>().ReverseMap();
            CreateMap<User, Login>().ReverseMap();
            CreateMap<User, LoginValidationDto>().ReverseMap();
            CreateMap<User, UserValidationDataDto>().ReverseMap();
        }
    }
}
