using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PadelBackend.Models.Auth;
using PadelBackend.Models.Auth.Dto;
using PadelBackend.Models.User.Dto;
using System.Web.Http.Description;

namespace PadelBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;

        public AuthController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        // Se comenta para evitar el error por no retornar nada

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public Task<ActionResult<LoginResponseDto>> Login([FromBody] Login logindata)
        //{
        //   /*
        //    Hay que verificar si el texto de entrada es el nombre de usuario o 
        //   la direccion de correo. Luego verficar si esta dentro de la base de datos para 
        //   luego ahi verificar si coincide con la contraseña dada.
        //   Una vez las credenciales son verificadas con exito se retorna el token creado mediante
        //   la funcion dentro del servicio de auth, para despues mediante este hacer 
        //   peticiones a paginas con encabezado AUTHORIZE.
        //   IMP: Hay que hacere el filtro de autorizaciones dentro del config
        //    */
        //}
    }
}
