using AutoMapper;
using PadelBackend.Models.Rackets;
using PadelBackend.Models.Rackets.Dto;

namespace PadelBackend.Config
{
    public class Mapping : Profile
    {
        public Mapping() 
        { 
            CreateMap<Racket, RacketDto>().ReverseMap();
            CreateMap<Racket, RacketsDto>().ReverseMap();
            CreateMap<CreateRacketDto, Racket>().ReverseMap();
            //update sin nulls
            CreateMap<UpdateRacketDto, RacketsDto>().ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));
        }

    }
}
