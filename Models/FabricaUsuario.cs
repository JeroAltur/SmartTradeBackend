using SamartTradeBackend.Models.CarroCompra;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;
using SmartTradeBackend.Services;

namespace SmartTradeBackend.Models
{
    public class FabricaUsuario
    {
        private readonly ServicioBD bd;
        public FabricaUsuario(ServicioBD servicio)
        {
            bd = servicio;
        }
        public void crearProducto(string tipo, Usuario u)
        {
            
            ListaDeseos deseos = new ListaDeseos();
            int iddeseos = bd.Insertar(deseos);
            u.id_Deseos = iddeseos;
            CarroCompra carroCompra = new CarroCompra();
            int idcarro = bd.Insertar(carroCompra);
            u.id_Carro = idcarro;
            int iduser = bd.Insertar(u);

            if (tipo == "cliente")
            {
                Cliente usuario = new Cliente(iduser);
                bd.Insertar(usuario);
            }
            if (tipo == "vendedor")
            {
                Vendedor usuario = new Vendedor(iduser);
                bd.Insertar(usuario);
            }
            if (tipo == "tecnico")
            {
                Tecnico usuario = new Tecnico(iduser);
                bd.Insertar(usuario);
            }
        }
    }
}
