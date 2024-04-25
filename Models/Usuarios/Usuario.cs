using SamartTradeBackend.Models.CarroCompras;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;
using SmartTradeBackend.Services;

namespace SamartTradeBackend.Models.Usuarios
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string contraseña { get; set; }
        public CarroCompra carrito { get; set; }
        public Deseos listaDeseos { get; set; }

        public Usuario() 
        { 
            carrito = new CarroCompra();
            listaDeseos = new Deseos();
        }

        public Usuario(string nombre, string correo, string direccion, string contraseña)
        {
            this.nombre = nombre;
            this.correo = correo;
            this.direccion = direccion;
            this.contraseña = contraseña;
        }
    }
}
