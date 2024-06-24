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
        // SE ELIMINA PARA LA V2
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UsersDto>>> Get()
        {
            try
            {
                var users = await userServices.GetManyUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
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
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> Post(CreateUserDto createUser)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await userServices.CreateOneUser(createUser));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> Put(int id, [FromBody] UpdateUserDto updateUser)
        {
            var idFromToken = User.FindFirst("id")?.Value;
            if ((idFromToken == null) || (!int.TryParse(idFromToken, out int idParsed)))
            {
                return Unauthorized(new {message = "Request denegated. Unauthorized"});
            }
            if(int.Parse(idFromToken) != id)
            {
                return Forbid();
            }
            try
            {
                return Ok(await userServices.UpdateOneUser(updateUser, id));
            }
            catch(NotFoundCustomEx ex)
            {
                return NotFound(new { message = ex.errorMessageDetails ?? $"The user with id '{id}' was not founded"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }   
}
