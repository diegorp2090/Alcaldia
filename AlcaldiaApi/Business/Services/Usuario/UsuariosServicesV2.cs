using AlcaldiaApi.Business.Interfaces;
using AlcaldiaApi.Domain.Entities.DTO;
using Microsoft.AspNetCore.Identity;

namespace AlcaldiaApi.Business.Services.Usuario
{
    public class UsuariosServicesV2 : IUsuarioServices
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsuariosServicesV2(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        public Task<(bool, dynamic)> CreateUserAsync(UsuarioDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, dynamic)> EsAdministrador(string UsuarioId)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> ObtenerTodosUsuarios() => _userManager.Users.Take(1).ToList();

        public Task<(bool, dynamic)> RemoverAdminitrador(string UsuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
