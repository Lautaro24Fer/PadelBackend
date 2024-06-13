using AutoMapper;
using PadelBackend.Models.User.Dto;
using PadelBackend.Models.User;

namespace PadelBackend.Config
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UsersDto>().ReverseMap();
        }
    }
}
