using SamartTradeBackend.Models.CarroCompras;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;
using SamartTradeBackend.Models.Productos;
using SmartTradeBackend.Services;

namespace SamartTradeBackend.Models.Usuarios
{
    public class Vendedor: Usuario
    {
        //public List<Producto> productos { get; set; }

        public Vendedor() { 
            //productos = new List<Producto>();
        }

        public Vendedor(Usuario u)
        {
            DNI = u.DNI;
            nombre = u.nombre;
            correo = u.correo;
            direccion = u.direccion;
            contraseña = u.contraseña;
            carrito = u.carrito;
            listaDeseos = u.listaDeseos;
            notificaciones = u.notificaciones;
            pedidos = u.pedidos;
        }
    }
}
