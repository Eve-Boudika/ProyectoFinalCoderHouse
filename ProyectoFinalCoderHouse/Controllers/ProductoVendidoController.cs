using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Repository;
using ProyectoFinalCoderHouse.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoFinalCoderHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        IConfiguration configuration;
        ProductoVendidoHandler productoVendidoHandler;
        public ProductoVendidoController(IConfiguration configuration)
        {
            this.configuration = configuration;
            productoVendidoHandler = new ProductoVendidoHandler();
        }
        // GET: api/<ProductoVendidoController>
        [HttpGet]
        public IEnumerable<Producto> Get(int idUsuario)
        {
            return productoVendidoHandler.TraerProductosVendidos(idUsuario, configuration);
        }
    }
}
