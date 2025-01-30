namespace AlcaldiaApi.Business.Interfaces
{
    using AlcaldiaApi.Domain.Entities.DTO;
    public interface IUsuarioServices
    {
        Task<(bool, dynamic)> CreateUserAsync(UsuarioDTO model);
        Task<(bool, dynamic)> EsAdministrador(string UsuarioId);
        Task<dynamic> ObtenerTodosUsuarios();
        Task<(bool, dynamic)> RemoverAdminitrador(string UsuarioId);
        //Task<List<User>> GetAsync(string filter);

        //Task<(bool succeded, dynamic entity)> CreateAsync(UserViewModel model);

        //Task<bool> AssignClaimAsync(AssignClaimViewModel model);

        //Task<ICollection<string>> GetClaimsAsync(string id);

        //Task<bool> DisableAsync(string id);

        //Task<bool> DeleteAsync(UserViewModel model);

        //Task<User> GetUserByUserNameAsync(string userName);

        //Task<(bool succeded, dynamic result)> UpdateUserAsync(User user);

        //Task<(bool succeeded, dynamic result)> ChangePassAsync(UserViewModel model);
    }
}
