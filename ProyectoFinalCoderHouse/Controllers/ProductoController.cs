using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Models;
using ProyectoFinalCoderHouse.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoFinalCoderHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        ProductoHandler productoHandler;

        public ProductoController(IConfiguration configuration)
        {
            productoHandler = new ProductoHandler(configuration);
        }

        // GET: api/<ProductoController>
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            return productoHandler.ListaDeProductos();
        }


        // POST api/<ProductoController>
        [HttpPost]
        public bool Post([FromBody] Producto producto)
        {
            return productoHandler.CrearProducto(producto);
        }

        // PUT api/<ProductoController>/5
        [HttpPut]
        public bool Put([FromBody] Producto producto)
        {
            return productoHandler.ModificarProducto(producto);
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete]
        public bool Delete(int Id)
        {
            return productoHandler.EliminarProducto(Id);
        }
    }
}
