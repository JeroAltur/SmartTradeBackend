using SamartTradeBackend.Models.Productos;
using SamartTradeBackend.Models.Usuarios;
using SmartTradeBackend.Services;

namespace SamartTradeBackend.Models
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
            Valoracion valoracion = new Valoracion();
            int idValoracion = bd.Insertar(valoracion);
            Console.WriteLine(valoracion.idValoracion);
            p.id_valoracion = idValoracion;

            if (tipo == "ropa")
            {
                int idprod = bd.Insertar(p);
                Ropa nuevoProducto = new Ropa(idprod);
                bd.Insertar(nuevoProducto);
            }
            if (tipo == "comida")
            {
                int idprod = bd.Insertar(p);
                Ropa nuevoProducto = new Ropa(idprod);
                bd.Insertar(nuevoProducto);
            }
            if (tipo == "electronica")
            {
                int idprod = bd.Insertar(p);
                Ropa nuevoProducto = new Ropa(idprod);
                bd.Insertar(nuevoProducto);
            }
        }
    }
}
