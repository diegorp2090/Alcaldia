namespace AlcaldiaApi.Business.Services.Login
{
    using AlcaldiaApi.Business.Interfaces;
    using AlcaldiaApi.Domain.Entities.DTO;
    using AlcaldiaApi.Domain.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class LoginServices : ILoginServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public LoginServices(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<(bool, dynamic)> LoginAsync(LoginDTO model)
        {
            var resultado = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            bool valido = false;

            if (resultado.Succeeded)
            {
                valido = true;
                return (valido, await ConstruirToken(model));
            }
            else
                return (valido, new RespuestasGenerica { EsValido = false, MensajeExcepcion = "Login error" });
        }

        private async Task<RespuestaAutenticacion> ConstruirToken(LoginDTO model)
        {
            RespuestaAutenticacion respuestaAutenticacion = new RespuestaAutenticacion();

            List<Claim> claims = new List<Claim>() { new Claim("email", model.Email) };

            var usuario = await _userManager.FindByEmailAsync(model.Email);
            var claimsDB = await _userManager.GetClaimsAsync(usuario);

            if (claimsDB != null && claimsDB.Any())
                claims.AddRange(claimsDB);

            if (!string.IsNullOrEmpty(_configuration["jtw"]))
            {
                var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jtw"]));
                var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

                var expiracion = DateTime.UtcNow.AddHours(1);

                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expiracion,
                    signingCredentials: creds)
                    ;

                respuestaAutenticacion = new RespuestaAutenticacion
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiracion = expiracion,
                };
            }

            return respuestaAutenticacion;
        }
    }
}
