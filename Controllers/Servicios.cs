using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartTrade.Services;
using SmartTradeBackend.Models;
using SmartTradeBackend.Services;
using static SamartTradeBackend.Controllers.HolaMundo;

namespace SamartTradeBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Servicios : ControllerBase
    {
        private readonly string dbPath;
        private readonly ServicioBD servicioBD;
        private readonly SmartTradeServices servicio;

        public Servicios()
        {
            dbPath = InicializacionServicioBD.GetDatabasePath();
            servicioBD = new ServicioBD(dbPath);
            servicio = new SmartTradeServices(servicioBD);
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
        public string PostCrearProducto(Producto prod, string tipo)
        {
            servicio.AgregarProductoDirecto(prod, tipo);
            return "Producto Añadido";
        }

        [HttpGet("BuscarPorId/{id}")]
        public string GetBuscarPorId(int id)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.ProductoPorId(id));
            return respuesta;
        }

        [HttpPost("AgregarValoracion/{valor}")]
        public string PostAgregarValoracion(Producto p, int valor)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.AgregarValoracion(p, valor));
            return respuesta;
        }


    }
}
