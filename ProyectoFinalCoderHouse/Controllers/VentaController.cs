using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Models;
using ProyectoFinalCoderHouse.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoFinalCoderHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        VentaHandler ventaHandler;

        public VentaController(IConfiguration configuration)
        {
            ventaHandler = new VentaHandler(configuration);
        }


        //GET: api/<VentaController>
        [HttpGet]
        public List<RelacionProductosVenta> Get()
        {
            return ventaHandler.TraerVentas();
        }


        // POST api/<VentaController>
        [HttpPost]
        public bool Post([FromBody] List<Producto> listaProductos, int NIdU)
        {
            if (listaProductos.Count > 0)
            {
                return ventaHandler.CargarVenta(listaProductos, NIdU);
            }
            return false;
        }


        // DELETE api/<VentaController>/5
        [HttpDelete]
        public bool Delete(int id)
        {
            return ventaHandler.EliminarVenta(id);
        }


    }


}
