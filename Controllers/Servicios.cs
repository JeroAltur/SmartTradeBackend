using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartTradeBackend.Services;
using SamartTradeBackend;
using SamartTradeBackend.Models.Productos;

namespace SamartTradeBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Servicios : ControllerBase
    {
        private readonly SmartTradeServices servicio;

        public Servicios(SmartTradeServices serviciobase)
        {
            servicio = serviciobase;
        }

        //--------------------------------------------

        //Productos
        [HttpGet("Todo")]
        public string GetTodo()
        {
            string respuesta = JsonConvert.SerializeObject(servicio.TodoProducto());
            return respuesta;
        }

        [HttpGet("Tendencia")]
        public string GetTendencia()
        {
            string respuesta = JsonConvert.SerializeObject(servicio.Tendencias());
            return respuesta;
        }

        [HttpGet("MejorValorado")]
        public string GetMejorValorado()
        {
            string respuesta = JsonConvert.SerializeObject(servicio.MejorValorado());
            return respuesta;
        }

        [HttpGet("CompradosIronman")]
        public string GetCompradosIronman()
        {
            string respuesta = JsonConvert.SerializeObject(servicio.CompradosPorIronman());
            return respuesta;
        }

        [HttpGet("Buscador/{nombre}")]
        public string GetBuscador(string nombre)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.Buscador(nombre));
            return respuesta;
        }

        [HttpPatch("CrearProducto/{name}/{description}/{price}/{imagen}/{huella}/{tipo}")] 
        public string PatchCrearProducto(string name, string description, double price, string imagenes, double huella, string tipo)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.AgregarProducto(name, description, price, imagenes, huella, tipo));
            return respuesta;
        }

        [HttpPost("AgregarValoracion/{id}/{valor}")]
        public string PostAgregarValoracion(int id, int valor)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.AgregarValoracion(id, valor));
            return respuesta;
        }

        //Usuarios
        [HttpPatch("CrearUsuario/{dni}/{nombre}/{correo}/{direccion}/{contraseña}/{tipo}")]
        public string PatchCrearUsuario(int dni, string nombre, string correo, string direccion, string contraseña, string tipo)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.AgregarUsuario(dni, nombre, correo, direccion, contraseña, tipo));
            return respuesta;
        }

        [HttpGet("Login/{correo}/{contraseña}")]
        public string Getlogin(string correo, string contraseña)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.Loguearse(correo, contraseña));
            return respuesta;
        }

        //Lista deseos
        [HttpPost("AñadirDeseado/{iduser}/{idprod}")]
        public string PostAñadirDeseado(int iduser, int idprod)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.AñadirListaDeseos(iduser, idprod));
            return respuesta;
        }

        [HttpPost("BorrarDeseado/{iduser}/{idprod}")]
        public string PostBorrrarDeseado(int iduser, int idprod)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.EliminarListaDeseos(iduser, idprod));
            return respuesta;
        }

        //Carrito
        [HttpPost("AñadirCarrito/{iduser}/{idprod}")]
        public string PostAñadirCarrito(int iduser, int idprod)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.AñadirCarrito(iduser, idprod));
            return respuesta;
        }

        [HttpPost("BorrarCarrito/{iduser}/{idprod}")]
        public string PostBorrrarCarrito(int iduser, int idprod)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.EliminarCarrito(iduser, idprod));
            return respuesta;
        }

    }
}
