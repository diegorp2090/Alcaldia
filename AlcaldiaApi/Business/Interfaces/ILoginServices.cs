namespace AlcaldiaApi.Business.Interfaces
{
    using AlcaldiaApi.Domain.Entities.DTO;
    public interface ILoginServices
    {
        Task<(bool, dynamic)> LoginAsync(LoginDTO model);
    }
}
