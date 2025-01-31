namespace AlcaldiaApi.Business.Interfaces
{
    using AlcaldiaApi.DTOs;

    public interface ILoginServices
    {
        Task<(bool, dynamic)> LoginAsync(LoginDTO model);
    }
}
