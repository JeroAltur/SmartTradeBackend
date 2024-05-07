using SamartTradeBackend.Models.CarroCompras;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;
using SmartTradeBackend.Services;

namespace SamartTradeBackend.Models.Usuarios
{
    public class Usuario
    {
        public int DNI { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string contraseña { get; set; }
        public CarroCompra carrito { get; set; }
        public Deseos listaDeseos { get; set; }
        public List<Notificaciones> notificaciones { get; set; }
        public List<Pedidos> pedidos { get; set; }

        public Usuario() 
        { 
            carrito = new CarroCompra();
            listaDeseos = new Deseos();
            notificaciones = new List<Notificaciones>();
            pedidos = new List<Pedidos>();
        }

        public Usuario(int dni, string nombre, string correo, string direccion, string contraseña) : this()
        {
            this.DNI = dni;
            this.nombre = nombre;
            this.correo = correo;
            this.direccion = direccion;
            this.contraseña = contraseña;
        }
    }
}
