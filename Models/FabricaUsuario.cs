using SamartTradeBackend.Models;
using SamartTradeBackend.Models.CarroCompras;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;
using SmartTradeBackend.Services;

namespace SmartTradeBackend.Models
{
    public class FabricaUsuario
    {
        public FabricaUsuario()
        {
            
        }
        public void crearUsuario(Usuario u, string tipo, Tienda t)
        {

            if (tipo == "cliente")
            {
                Cliente usuario = new Cliente(u);
                usuario.carrito.idCarro = ++t.ultimoIdCarrito;
                usuario.listaDeseos.idDeseos = ++t.ultimoIdDeseos;
                t.Clientes.Add(usuario);
            }
            if (tipo == "vendedor")
            {
                Vendedor usuario = new Vendedor(u);
                usuario.carrito.idCarro = ++t.ultimoIdCarrito;
                usuario.listaDeseos.idDeseos = ++t.ultimoIdDeseos;
                t.Vendedores.Add(usuario);
            }
            if (tipo == "tecnico")
            {
                Tecnico usuario = new Tecnico(u);
                usuario.carrito.idCarro = ++t.ultimoIdCarrito;
                usuario.listaDeseos.idDeseos = ++t.ultimoIdDeseos;
                t.Tecnicos.Add(usuario);
            }
        }
    }
}
