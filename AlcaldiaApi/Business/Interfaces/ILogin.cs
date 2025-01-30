namespace AlcaldiaApi.Business.Interfaces
{
    using AlcaldiaApi.Domain.Entities.DTO;
    public interface ILogin
    {
        Task<(bool, dynamic)> CreateUserAsync(LoginDTO model);
        Task<(bool, dynamic)> LoginAsync(LoginDTO model);
    }
}
