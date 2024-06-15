using Microsoft.AspNetCore.Mvc;
using PadelBackend.Models.User.Dto;
using PadelBackend.Services;

namespace PadelBackend.Controllers
{
    [ApiController]
    [Route("api/user")]
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
        public async Task<ActionResult<List<UsersDto>>> Get()
        {
            try
            {
                var users = await userServices.GetManyUsers();
                return Ok(users);
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UsersDto>>> Get(int id)
        {
            try
            {
                var user = await userServices.GetOneUser(id);
                if(user == null)
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }
    }   
}
