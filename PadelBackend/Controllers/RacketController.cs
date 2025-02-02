﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PadelBackend.Exceptions;
using PadelBackend.Models.Query.Dto;
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
        public async Task<ActionResult<List<RacketsDto>>> Get([FromQuery] QueryDto query)
        {
            try
            {
                var rackets = await racketServices.GetManyRackets(query);
                return Ok( new {status = true, requestResponse = rackets, messageDetails = "The request was answered successfully" });
            }
            catch(Exception ex)
            {
                return BadRequest(new {status = false, messageDetails = ex.Message});
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
                return Ok(new {status = true, requestResponse = racket, messageDetails = "The request was answered successfully" });
            }
            catch(NotFoundCustomEx ex)
            {
                return NotFound(new {status = false, messageDetails = ex.errorMessageDetails ?? $"Racket with id {id} not founded"});
            }
            catch(Exception ex)
            {
                return BadRequest(new {status = false, messageDetails = ex.Message});
            }
        }
    }
}
