using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartTradeBackend.Services;
using SamartTradeBackend;
using SamartTradeBackend.Models.Productos;
using SamartTradeBackend.Models.Usuarios;

namespace SamartTradeBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Servicios : ControllerBase
    {
        private readonly SmartTradeServices servicio;

        public Servicios()
        {
            servicio = SmartTradeServices.Instance; ;
        }

        //--------------------------------------------

        //Productos
        [HttpGet("Todo")]
        public string GetTodo()
        {
            return JsonConvert.SerializeObject(servicio.TodoProducto());
        }

        [HttpGet("TodoPorValidar")]
        public string GetTodoPorValidar()
        {
            return JsonConvert.SerializeObject(servicio.TodoPorValidar());
        }

        [HttpGet("Tendencia")]
        public string GetTendencia()
        {
            return JsonConvert.SerializeObject(servicio.Tendencias());
        }

        [HttpGet("MejorValorado")]
        public string GetMejorValorado()
        {
            return JsonConvert.SerializeObject(servicio.MejorValorado());
        }

        [HttpGet("Buscador/{nombre}")]
        public string GetBuscador(string nombre)
        {
            return JsonConvert.SerializeObject(servicio.Buscador(nombre));
        }

        [HttpPatch("SolicitarProducto/{name}/{description}/{price}/{imagenes}/{huella}/{tipo}/{dni}")]
        public string PatchSolicitarProducto(string name, string description, double price, string imagenes, double huella, string tipo, int dni)
        {
            return JsonConvert.SerializeObject(servicio.SolicitarProducto(name, description, price, imagenes, huella, tipo, dni));
        }

        [HttpPatch("CrearProducto/{id}")] 
        public string PatchCrearProducto(int id)
        {
            return JsonConvert.SerializeObject(servicio.AceptarProducto(id));
        }

        [HttpPost("AgregarValoracion/{id}/{valor}")]
        public string PostAgregarValoracion(int id, int valor)
        {
            return JsonConvert.SerializeObject(servicio.AgregarValoracion(id, valor));
        }

        //Usuarios
        [HttpPatch("CrearUsuario/{dni}/{nombre}/{correo}/{direccion}/{contraseña}/{tipo}")]
        public string PatchCrearUsuario(int dni, string nombre, string correo, string direccion, string contraseña, string tipo)
        {
            return JsonConvert.SerializeObject(servicio.AgregarUsuario(dni, nombre, correo, direccion, contraseña, tipo));
        }

        [HttpGet("Login/{correo}/{contraseña}")]
        public string Getlogin(string correo, string contraseña)
        {
            Usuario? u = servicio.Loguearse(correo, contraseña);
            string respuesta;

            if (u == null)
            {
                respuesta = JsonConvert.SerializeObject("Correo o contraseña incorrectos.");
            }
            else
            {
                respuesta = JsonConvert.SerializeObject(u);
            }
            return respuesta;
        }

        [HttpGet("TipoUsuario/{dni}")]
        public string GetTipoUsuario(int dni)
        {
            return servicio.TipoUsuario(dni);
        }

        //Lista deseos
        [HttpPost("AñadirDeseado/{iduser}/{idprod}")]
        public string PostAñadirDeseado(int iduser, int idprod)
        {
            return JsonConvert.SerializeObject(servicio.AñadirListaDeseos(iduser, idprod));
        }

        [HttpPost("BorrarDeseado/{iduser}/{idprod}")]
        public string PostBorrrarDeseado(int iduser, int idprod)
        {
            return JsonConvert.SerializeObject(servicio.EliminarListaDeseos(iduser, idprod));
        }

        [HttpGet("Deseos/{iduser}")]
        public string GetDeseos(int iduser)
        {
            return JsonConvert.SerializeObject(servicio.Deseos(iduser));
        }

        //Carrito
        [HttpPost("AñadirCarrito/{iduser}/{idprod}")]
        public string PostAñadirCarrito(int iduser, int idprod)
        {
            return JsonConvert.SerializeObject(servicio.AñadirCarrito(iduser, idprod));
        }

        [HttpPost("BorrarCarrito/{iduser}/{idprod}")]
        public string PostBorrrarCarrito(int iduser, int idprod)
        {
            return JsonConvert.SerializeObject(servicio.EliminarCarrito(iduser, idprod));
        }

        [HttpGet("Carrito/{iduser}")]
        public string GetCarrito(int iduser)
        {
            return JsonConvert.SerializeObject(servicio.Carrito(iduser));
        }

        //Pedidos
        [HttpPost("RealizarPedido/{iduser}")]
        public string PostAñadirDeseado(int iduser)
        {
            return JsonConvert.SerializeObject(servicio.RealizarPedido(iduser));
        }

        [HttpGet("Pedidos/{iduser}")]
        public string GetPedidos(int iduser)
        {
            return JsonConvert.SerializeObject(servicio.ListaPedidos(iduser));
        }

        //Notificaciones
        [HttpPost("NotificacionLeida/{iduser}/{idnoti}")]
        public string PostNotificacionLeida(int iduser, int idnoti)
        {
            servicio.LeerNotificacion(iduser, idnoti);
            return "Notificacion leida.";
        }

        [HttpPost("BorrarNotificacion/{iduser}/{idnoti}")]
        public string PostBorrarNotificacion(int iduser, int idnoti)
        {
            return JsonConvert.SerializeObject(servicio.BorrarNotificacion(iduser, idnoti));
        }

        [HttpGet("Notificaciones/{iduser}")]
        public string GetNotificaciones(int iduser)
        {
            return JsonConvert.SerializeObject(servicio.ListaNotificaciones(iduser));
        }

        //Cosas Probar
        [HttpGet("TodoCliente")]
        public string GetTodoCliente()
        {
            return JsonConvert.SerializeObject(servicio.TodoCliente());
        }

        [HttpGet("TodoVendedor")]
        public string GetTodoVendedor()
        {
            return JsonConvert.SerializeObject(servicio.TodoVendedor());
        }

        [HttpGet("TodoTecnico")]
        public string GetTodoTecnico()
        {
            return JsonConvert.SerializeObject(servicio.TodoTecnico());
        }

        [HttpPatch("CrearNotificacion/{dni}/{titulo}/{descripcion}")]
        public string PatchCrearNotificacion(int dni, string titulo, string descripcion)
        {
            servicio.CrearNotificacion(dni, titulo, descripcion);
            return "Notificacion creada";
        }

    }
}
