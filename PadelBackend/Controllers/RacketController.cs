﻿using Microsoft.AspNetCore.Mvc;
using PadelBackend.Exceptions;
using PadelBackend.Models.Racket.Dto;
using PadelBackend.Services;
using System.Net;
using System.Web.Http.Description;

namespace PadelBackend.Controllers
{
    [Route("api/racket")]
    [ApiController]
    public class RacketController : ControllerBase
    {
        private readonly IRacketServices racketServices;

        public RacketController(IRacketServices racketServices)
        {
            this.racketServices = racketServices;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<RacketsDto>>> Get()
        {
            try
            {
                var rackets = await racketServices.GetManyRackets();
                return Ok(rackets);
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RacketDto>> Get(int id)
        {
            try
            {
                var racket = await racketServices.GetOneRacket(id);
                return Ok(racket);
            }
            catch(NotFoundCustomEx ex)
            {
                return NotFound(new {message = ex.Message ?? $"Racket with id {id} not founded"});
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}
