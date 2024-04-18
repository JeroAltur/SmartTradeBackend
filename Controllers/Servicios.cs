using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartTrade.Services;
using SmartTradeBackend.Models;
using SmartTradeBackend.Services;
using SamartTradeBackend;

namespace SamartTradeBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Servicios : ControllerBase
    {
        Conexion conexion = new Conexion();
        private readonly ServicioBD servicioBD;
        private readonly SmartTradeServices servicio;

        public Servicios()
        {
            servicioBD = new ServicioBD(conexion.GetConexion());
            servicio = new SmartTradeServices(servicioBD);
        }

        [HttpGet("BorrarTablas")]
        public string GetBorrarTablas()
        {
            servicio.Borrar();
            return "BD borrada";
        }

        [HttpGet("CrearTablas")]
        public string Crear()
        {
            servicio.Borrar();
            return "BD borrada";
        }

        [HttpPost("IniciarBD")]
        public string PostIniciarBD()
        {
            servicio.IniciarBD();
            return "BD iniciada";
        }

        [HttpGet("Todo")]
        public string GetTodo()
        {
            string respuesta = JsonConvert.SerializeObject(servicio.Todo());
            return respuesta;
        }

        [HttpGet("TodoValoracion")]
        public string GetTodoValoracion()
        {
            string respuesta = JsonConvert.SerializeObject(servicio.TodoValoracion());
            return respuesta;
        }

        [HttpGet("Buscador/{nombre}")]
        public string GetBuscador(string nombre)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.Buscador(nombre));
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

        [HttpPost("CrearProducto/{tipo}")]
        public string PostCrearProducto(string name, string description, double price, string imagenes, double huella, string tipo)
        {
            servicio.AgregarProducto(name, description, price, imagenes, huella, tipo);
            return "Producto Añadido";
        }

        [HttpGet("BuscarPorId/{id}")]
        public string GetBuscarPorId(int id)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.ProductoPorId(id));
            return respuesta;
        }

        [HttpPost("AgregarValoracion/{id}/{valor}")]
        public string PostAgregarValoracion(int id, int valor)
        {
            Producto p = servicio.ProductoPorId(id);
            string respuesta = JsonConvert.SerializeObject(servicio.AgregarValoracion(p, valor));
            return respuesta;
        }


    }
}
