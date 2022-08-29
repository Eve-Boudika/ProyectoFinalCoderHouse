using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Models;
using ProyectoFinalCoderHouse.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoFinalCoderHouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        UsuarioHandler usuarioHandler;

        public UsuarioController(IConfiguration configuration)
        {
            usuarioHandler = new UsuarioHandler(configuration);
        }


        // GET: api/<UsuarioController>
        [HttpGet]
        public Usuario Get (string nombreUsuario)
        {
            return usuarioHandler.TraerUsuario(nombreUsuario);
        }


        // POST api/<UsuarioController>
        [HttpPost]
        public bool Post([FromBody] Usuario usuario)
        {
            return usuarioHandler.CrearUsuario(usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut]
        public bool Put([FromBody] Usuario usuario)
        {
            return usuarioHandler.ModificarUsuario(usuario);
        }


        // DELETE api/<UsuarioController>/5
        [HttpDelete]
        public bool Delete(long Id)
        {
            return usuarioHandler.EliminarUsuario(Id);
        }
    }
}
