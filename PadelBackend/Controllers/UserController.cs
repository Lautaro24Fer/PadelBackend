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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<UsersDto>>> Get()
        {
            try
            {
                var users = await userServices.GetManyUsers();
                return Ok(new {status = true, requestResponse = users, messageDetails = "The request was answered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, messageDetails = ex.Message });
            }
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<UsersDto>>> Get(int id)
        {
            try
            {
                var user = await userServices.GetOneUser(id);
                if (user == null)
                {
                    return NotFound(new { status = false, messageDetails = $"The user with '{id}' was not founded" });
                }
                return Ok(new { status = true, requestResponse = user, messageDetails = "The request was answered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, messageDetails = ex.Message });
            }
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
                return Ok(new { status = true, requestResponse = user, messageDetails = "The request was answered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, messageDetails = ex.Message });
            }
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> Put(int id, [FromBody] UpdateUserDto updateUser)
        {
            var idFromToken = User.FindFirst("id")?.Value;
            if ((idFromToken == null) || (!int.TryParse(idFromToken, out _)))
            {
                return Unauthorized(new {status = false, messageDetails = "Request denegated. Unauthorized"});
            }
            if(int.Parse(idFromToken) != id)
            {
                return Forbid("Can not update data from other user");
            }
            try
            {
                var user = await userServices.UpdateOneUser(updateUser, id);
                return Ok(new { status = false, requestResponse = user, messageDetails = "The request was answered successfully" });
            }
            catch(NotFoundCustomEx ex)
            {
                return NotFound(new { status = false, messageDetails = ex.errorMessageDetails ?? $"The user with id '{id}' was not founded"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, messageDetails = ex.Message });
            }
        }
    }   
}
