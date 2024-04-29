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

        [HttpGet("Todo")]
        public string GetTodo()
        {
            string respuesta = JsonConvert.SerializeObject(servicio.TodoProducto());
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

        [HttpPatch("CrearProducto/{tipo}")]
        public string PatchCrearProducto(string name, string description, double price, string imagenes, double huella, string tipo)
        {
            servicio.AgregarProducto(name, description, price, imagenes, huella, tipo);
            return "Producto Añadido";
        }

        [HttpPost("AgregarValoracion/{id}/{valor}")]
        public string PostAgregarValoracion(int id, int valor)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.AgregarValoracion(id, valor));
            return respuesta;
        }

    }
}
