using AlcaldiaApi.Business.Interfaces;

namespace AlcaldiaApi.Business.Services.Pruebas
{
    public class PruebasServices : IPruebasServices
    {
        private readonly int _valor;

        public int Valor 
        {
            get => _valor;
        }

        public PruebasServices()
        {
            _valor = new Random().Next(1000);
        }
    }
}
