using AlcaldiaApi.Business.Interfaces;
using AlcaldiaApi.Domain.Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlcaldiaApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin login;

        public LoginController(ILogin _login)
        {
            this.login = _login;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var (EsValido, result) = await login.LoginAsync(loginDTO);

                if (EsValido)
                    return Ok(result);
                else
                    return BadRequest(result);

            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }           
        }


        [HttpPost("crear")]
        public async Task<ActionResult> Crear([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var (EsValido, result) = await login.CreateUserAsync(loginDTO);

                if (EsValido)
                    return Ok(result);
                else
                    return BadRequest(result);                
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
