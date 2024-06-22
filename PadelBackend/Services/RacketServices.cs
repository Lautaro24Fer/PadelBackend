﻿using AutoMapper;
using PadelBackend.Models.Racket.Dto;
using PadelBackend.Repositories;
using PadelBackend.Exceptions;
using System.Net;
using System.Web.Http;

namespace PadelBackend.Services
{

    public interface IRacketServices
    {
        public Task<RacketDto> CreateRacket(); 
        public Task DeleteRacket();
        public Task<RacketDto> UpdateRacket();
        public Task<RacketDto> GetOneRacket(int id);
        public Task<List<RacketsDto>> GetManyRackets();
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

        public Task<RacketDto> CreateRacket()
        {
            throw new NotImplementedException();
        }
        public Task DeleteRacket()
        {
            throw new NotImplementedException();

        }
        public Task<RacketDto> UpdateRacket()
        {
            throw new NotImplementedException();

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


        public async Task<List<RacketsDto>> GetManyRackets()
        {
            var rackets = await racketRepository.Get();
            return mapper.Map<List<RacketsDto>>(rackets);
        }
    }
}
