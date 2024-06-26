using AutoMapper;

using PadelBackend.Models.User.Dto;
using PadelBackend.Models.User;
using PadelBackend.Models.Auth;
using PadelBackend.Models.Auth.Dto;
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
            CreateMap<UpdateUserDto, User>().ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));
            // Racketas
            CreateMap<Racket, RacketDto>().ReverseMap();
            CreateMap<Racket, RacketsDto>().ReverseMap();
        }
    }
}
