using AutoMapper;
using PadelBackend.Models.Racket.Dto;
using PadelBackend.Repositories;
using PadelBackend.Exceptions;
using System.Net;
using System.Web.Http;
using PadelBackend.Models.Racket;
using PadelBackend.Models.Query.Dto;

namespace PadelBackend.Services
{

    public interface IRacketServices
    {
        public Task<RacketDto> GetOneRacket(int id);
        public Task<List<RacketsDto>> GetManyRackets(QueryDto query);
    }
    public class RacketServices : IRacketServices
    {
        public readonly IRacketRepository racketRepository;
        public readonly IEncoderService encoderService;
        public readonly IMapper mapper;
        public RacketServices(IRacketRepository racketRepository, IEncoderService encoderService, IMapper mapper) 
        {
            this.racketRepository = racketRepository;
            this.encoderService = encoderService;
            this.mapper = mapper;
        }
        public async Task<RacketDto> GetOneRacket(int id)
        {
            var racket = await racketRepository.GetOne(r => r.Id == id);
            if (racket == null)
            {
                throw new NotFoundCustomEx();
            }
            return mapper.Map<RacketDto>(racket);
        }
        public async Task<List<RacketsDto>> GetManyRackets(QueryDto query)
        {
            Console.WriteLine($"{query}");
            var rackets = await racketRepository.Get();
            return mapper.Map<List<RacketsDto>>(rackets);
        }
    }
}
