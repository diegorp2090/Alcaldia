using AlcaldiaApi.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace AlcaldiaApi.Controllers
{
    [Route("api/pruebas")]
    [ApiController]
    public class PruebasController : Controller
    {
        private readonly IPruebasServices _singletoServices;
        private readonly IPruebasServices _scopedServices;
        private readonly IPruebasServices _trasientServices;

        private readonly IPruebasServices _singletoServicesV2;
        private readonly IPruebasServices _scopedServicesV2;
        private readonly IPruebasServices _trasientServicesV2;

        public PruebasController(
            [FromKeyedServices("pruebasSingleton")]IPruebasServices singletoServices,
            [FromKeyedServices("pruebasScoped")]IPruebasServices scopedServices,
            [FromKeyedServices("pruebasTransient")]IPruebasServices trasientServices,
            [FromKeyedServices("pruebasSingleton")] IPruebasServices singletoServicesV2,
            [FromKeyedServices("pruebasScoped")] IPruebasServices scopedServicesV2,
            [FromKeyedServices("pruebasTransient")] IPruebasServices trasientServicesV2
            )
        {
            this._singletoServices = singletoServices;
            this._scopedServices = scopedServices;
            this._trasientServices = trasientServices;
            
            this._singletoServicesV2 = singletoServicesV2;
            this._scopedServicesV2 = scopedServicesV2;
            this._trasientServicesV2 = trasientServicesV2;
        }


        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();
            result.Add("Singleton", _singletoServices.Valor);
            result.Add("Scoped", _scopedServices.Valor);
            result.Add("Trasient", _trasientServices.Valor);

            result.Add("SingletonV2", _singletoServicesV2.Valor);
            result.Add("ScopedV2", _scopedServicesV2.Valor);
            result.Add("TrasientV2", _trasientServicesV2.Valor);

            return result;
        }
    }
}
