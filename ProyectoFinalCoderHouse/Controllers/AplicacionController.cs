using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Models;
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
        UsuarioHandler usuarioHandler;
        public AplicacionController(IConfiguration configuration)
        {
            this.configuration = configuration;
            aplicacionRepositorio = new AplicacionHandler(configuration);
            usuarioHandler = new UsuarioHandler(configuration);
        }


        // GET: api/<AplicacionController>
        [HttpGet]
        public string Get()
        {
            return aplicacionRepositorio.TraerNombre();
        }


        // GET: api/<AplicacionController>
        [HttpGet]
        [Route("/ValidarSesion")]
        public object Get(string nombreUsuario, string contraseña)
        {
            var usuarioIngresado = usuarioHandler.ValidarSesion(nombreUsuario, contraseña);
            object respuesta = null;

            if (usuarioIngresado.Id == 0)
            {
                respuesta = new { respuesta = "Usuario o contraseña incorrectos", usuario = usuarioIngresado };
                return respuesta;
            }
            respuesta = new { respuesta = "Sesión Validada", usuario = usuarioIngresado };
            return respuesta;
        }


    }
}
