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
        public void crearProducto(string tipo, Usuario u)
        {

            if (tipo == "cliente")
            {
                Cliente usuario = new Cliente(u);
            }
            if (tipo == "vendedor")
            {
                Vendedor usuario = new Vendedor(u);
            }
            if (tipo == "tecnico")
            {
                Tecnico usuario = new Tecnico(u);
            }
        }
    }
}
