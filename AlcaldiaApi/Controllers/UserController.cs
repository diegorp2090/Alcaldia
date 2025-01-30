namespace AlcaldiaApi.Controllers
{
    using AlcaldiaApi.Business.Interfaces;
    using AlcaldiaApi.Domain.Entities.DTO;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    [Route("api/usuario")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly IUsuariosProvider usuariosProvider;

        public UserController(IUsuariosProvider _usuariosProvider)
        {
            usuariosProvider = _usuariosProvider;
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Crear([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                var (EsValido, result) = await usuariosProvider.CreateUserAsync(usuarioDTO);

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

        [HttpPost("HacerAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> HacerAdmin([FromBody]string UsuarioId)
        {
            try
            {
                var (EsValido, result) = await usuariosProvider.EsAdministrador(UsuarioId);

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

        [HttpPost("RemoverAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> RemoverAdmin([FromBody] string UsuarioId)
        {
            try
            {
                var (EsValido, result) = await usuariosProvider.RemoverAdminitrador(UsuarioId);

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
