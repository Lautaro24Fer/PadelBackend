using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PadelBackend.Models.Auth;
using PadelBackend.Models.Auth.Dto;
using PadelBackend.Models.User;
using PadelBackend.Models.User.Dto;
using PadelBackend.Services;
using System.Web.Http.Description;

namespace PadelBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUsersServices usersServices;
        private readonly IAuthServices authServices;

        public AuthController(IMapper mapper, IUsersServices usersServices, IAuthServices authServices)
        {
            this.mapper = mapper;
            this.usersServices = usersServices;
            this.authServices = authServices;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] Login logindata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userstate = await usersServices.ValidateCredentials(logindata);
                if (userstate.Status)
                {
                    return BadRequest(new {message = "Credentials do not match" });
                }

                var token = authServices.GenerateJwtToken(userstate.User);

                return Ok(new LoginResponseDto
                {
                    Token = token,
                    User = mapper.Map<UserLoginResponseDto>(userstate.User)
                });

            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
            
        }
    }
}
