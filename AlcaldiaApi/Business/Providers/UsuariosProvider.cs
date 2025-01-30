namespace AlcaldiaApi.Business.Providers
{
    using AlcaldiaApi.Business.Interfaces;
    using AlcaldiaApi.Domain.Entities.DTO;
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;

    public class UsuariosProvider : IUsuariosProvider
    {
        private readonly UserManager<IdentityUser> userManager;

        public UsuariosProvider(UserManager<IdentityUser> _userManager)
        {
            userManager = _userManager;
        }

        public async Task<(bool, dynamic)> CreateUserAsync(UsuarioDTO model)
        {
            bool EsValido = false;
            try
            {
                var user = new IdentityUser { UserName = model.UserName, Email = model.Email };                
                var result = await userManager.CreateAsync(user, model.Password);

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
            var usuario = await userManager.FindByIdAsync(UsuarioId);

            if (usuario != null)
                await userManager.AddClaimAsync(usuario, new Claim("role", "Admin"));
            else
                return (false, "El UsuarioId no existe");

            return (true, $"Se actualizo correctamente");
        }

        public async Task<(bool, dynamic)> RemoverAdminitrador(string UsuarioId)
        {
            var usuario = await userManager.FindByIdAsync(UsuarioId);

            if (usuario != null)
                await userManager.RemoveClaimAsync(usuario, new Claim("role", "Admin"));
            else
                return (false, "El UsuarioId no existe");

            return (true, $"Se removio correctamente");
        }
    }
}
