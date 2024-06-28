using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PadelBackend.Exceptions;
using PadelBackend.Models.User.Dto;
using PadelBackend.Services;

namespace PadelBackend.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUsersServices userServices;

        public UserController(IUsersServices userServices)
        {
            this.userServices = userServices;
        }
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Post(CreateUserDto createUser)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(new {status = false, requestResponse = ModelState, messageDetails = "Bad request. ModelState invalid"});
            }
            try
            {
                var user = await userServices.CreateOneUser(createUser);
                return Created("New user", new { status = true, requestResponse = user, messageDetails = "The request was answered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, messageDetails = ex.Message });
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> Put([FromBody] UpdateUserDto updateUser)
        {
            var idFromToken = User.FindFirst("id")?.Value;
            var idFromTokenIsInt = int.TryParse(idFromToken, out int idIntParsed);
            if ((idFromToken == null) || (!idFromTokenIsInt))
            {
                return Unauthorized(new {status = false, messageDetails = "Request denegated. Unauthorized"});
            }
            try
            {
                var user = await userServices.UpdateOneUser(updateUser, idIntParsed);
                return Ok(new { status = false, requestResponse = user, messageDetails = "The request was answered successfully" });
            }
            catch(NotFoundCustomEx ex)
            {
                return NotFound(new { status = false, messageDetails = ex.errorMessageDetails ?? $"The user with id '{idIntParsed}' was not founded"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, messageDetails = ex.Message });
            }
        }
    }   
}
