using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoFinalCoderHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacionController : ControllerBase
    {
        IConfiguration configuration;
        AplicacionHandler aplicacionRepositorio;
        public AplicacionController(IConfiguration configuration)
        {
            this.configuration = configuration;
            aplicacionRepositorio = new AplicacionHandler(configuration);
        }

        // GET: api/<AplicacionController>
        [HttpGet]
        public string Get()
        {
            return aplicacionRepositorio.TraerNombre();
        }

        
    }
}
