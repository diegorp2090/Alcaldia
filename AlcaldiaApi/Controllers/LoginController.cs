using AlcaldiaApi.Business.Interfaces;
using AlcaldiaApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlcaldiaApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginServices _loginServices;

        public LoginController(ILoginServices loginServices)
        {
            this._loginServices = loginServices;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var (EsValido, result) = await _loginServices.LoginAsync(loginDTO);

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
