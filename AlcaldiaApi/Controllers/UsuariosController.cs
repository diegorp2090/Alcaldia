namespace AlcaldiaApi.Controllers
{
    using AlcaldiaApi.Business.Interfaces;    
    using AlcaldiaApi.Domain.Entities.DTO;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/usuarios")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioServices _usuarioServices;

        public UsuariosController(IUsuarioServices usuarioServices)
        {
            this._usuarioServices = usuarioServices;
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Crear([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                var (EsValido, result) = await _usuarioServices.CreateUserAsync(usuarioDTO);

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
        public async Task<ActionResult> HacerAdmin([FromBody] string UsuarioId)
        {
            try
            {
                var (EsValido, result) = await _usuarioServices.EsAdministrador(UsuarioId);

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
                var (EsValido, result) = await _usuarioServices.RemoverAdminitrador(UsuarioId);

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

        [HttpGet("ObtenerTodosUsuarios")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> ObtenerTodosUsuarios()
        {
            try
            {
                var result = await _usuarioServices.ObtenerTodosUsuarios();

                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
