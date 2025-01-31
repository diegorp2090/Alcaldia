using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AlcaldiaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebasSyncController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            Thread.Sleep(1000);
            Console.WriteLine("Conexion a BD terminada");

            Thread.Sleep(1000);
            Console.WriteLine("envio email");
            
            Console.WriteLine("todo a terminado");

            stopwatch.Stop();

            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetASync()
        {
            Task task = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexion a BD terminada");
            });

            task.Start();
            await task;
            Console.WriteLine("envio email");            

            Console.WriteLine("todo a terminado");
            
            return Ok();
        }
    }
}
