using AlcaldiaApi.Business.Interfaces;
using AlcaldiaApi.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AlcaldiaApi.Business.Services.Usuario
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsuarioServices(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<(bool, dynamic)> CreateUserAsync(UsuarioDTO model)
        {
            bool EsValido = false;
            try
            {
                var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    EsValido = true;
                    return (EsValido, $"Se genero correctamente");
                }
                else
                {
                    return (EsValido, result.Errors);
                }
            }
            catch (Exception exception)
            {
                return (EsValido, $"{exception.Message}");
            }
        }

        public async Task<(bool, dynamic)> EsAdministrador(string UsuarioId)
        {
            var usuario = await _userManager.FindByIdAsync(UsuarioId);

            if (usuario != null)
                await _userManager.AddClaimAsync(usuario, new Claim("role", "Admin"));
            else
                return (false, "El UsuarioId no existe");

            return (true, $"Se actualizo correctamente");
        }        

        public async Task<(bool, dynamic)> RemoverAdminitrador(string UsuarioId)
        {
            var usuario = await _userManager.FindByIdAsync(UsuarioId);

            if (usuario != null)
                await _userManager.RemoveClaimAsync(usuario, new Claim("role", "Admin"));
            else
                return (false, "El UsuarioId no existe");

            return (true, $"Se actualizo correctamente");
        }

        public async Task<dynamic> ObtenerTodosUsuarios() => _userManager.Users.ToList();        
    }
}
