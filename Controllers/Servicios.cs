﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("TodoPorValidar")]
        public string GetTodoPorValidar()
        {
            string respuesta = JsonConvert.SerializeObject(servicio.TodoPorValidar());
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

        [HttpPatch("SolicitarProducto/{name}/{description}/{price}/{imagenes}/{huella}/{tipo}/{dni}")]
        public string PatchSolicitarProducto(string name, string description, double price, string imagenes, double huella, string tipo, int dni)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.SolicitarProducto(name, description, price, imagenes, huella, tipo, dni));
            return respuesta;
        }

        [HttpPatch("CrearProducto/{id}")] 
        public string PatchCrearProducto(int id)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.AceptarProducto(id));
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
            string respuesta = JsonConvert.SerializeObject(servicio.AñadirListaDeseos(iduser, idprod));
            return respuesta;
        }

        [HttpPost("BorrarDeseado/{iduser}/{idprod}")]
        public string PostBorrrarDeseado(int iduser, int idprod)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.EliminarListaDeseos(iduser, idprod));
            return respuesta;
        }

        [HttpGet("Deseos/{iduser}")]
        public string GetDeseos(int iduser)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.Deseos(iduser));
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

        [HttpGet("Carrito/{iduser}")]
        public string GetCarrito(int iduser)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.Carrito(iduser));
            return respuesta;
        }

        //Pedidos
        [HttpPost("RealizarPedido/{iduser}")]
        public string PostAñadirDeseado(int iduser)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.RealizarPedido(iduser));
            return respuesta;
        }

        [HttpGet("Pedidos/{iduser}")]
        public string GetPedidos(int iduser)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.ListaPedidos(iduser));
            return respuesta;
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
            string respuesta = JsonConvert.SerializeObject(servicio.BorrarNotificacion(iduser, idnoti));
            return respuesta;
        }

        [HttpGet("Notificaciones/{iduser}")]
        public string GetNotificaciones(int iduser)
        {
            string respuesta = JsonConvert.SerializeObject(servicio.ListaNotificaciones(iduser));
            return respuesta;
        }

        //Cosas Probar
        [HttpGet("TodoCliente")]
        public string GetTodoCliente()
        {
            string respuesta = JsonConvert.SerializeObject(servicio.TodoCliente());
            return respuesta;
        }

        [HttpGet("TodoVendedor")]
        public string GetTodoVendedor()
        {
            string respuesta = JsonConvert.SerializeObject(servicio.TodoVendedor());
            return respuesta;
        }

        [HttpGet("TodoTecnico")]
        public string GetTodoTecnico()
        {
            string respuesta = JsonConvert.SerializeObject(servicio.TodoTecnico());
            return respuesta;
        }

        [HttpPatch("CrearNotificacion/{dni}/{titulo}/{descripcion}")]
        public string PatchCrearNotificacion(int dni, string titulo, string descripcion)
        {
            servicio.CrearNotificacion(dni, titulo, descripcion);
            return "Notificacion creada";
        }

    }
}
