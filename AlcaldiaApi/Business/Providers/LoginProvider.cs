namespace AlcaldiaApi.Business.Providers
{
    using AlcaldiaApi.Business.Interfaces;
    using AlcaldiaApi.Domain.Entities.DTO;
    using AlcaldiaApi.Domain.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq.Expressions;
    using System.Security.Claims;
    using System.Text;

    public class LoginProvider : ILogin
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public IConfiguration configuration { get; }

        public LoginProvider(UserManager<IdentityUser> userManager
            , IConfiguration _configuration
            , SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = _configuration;
            this.signInManager = signInManager;
        }

        public async Task<(bool,dynamic)> LoginAsync(LoginDTO model)
        {
            var resultado = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent:false, lockoutOnFailure:false);
            bool valido = false;

            if(resultado.Succeeded)
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

            var usuario = await userManager.FindByEmailAsync(model.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            if (claimsDB != null && claimsDB.Any())
                claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jtw"]));
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

            return respuestaAutenticacion;
        }
    }
}
