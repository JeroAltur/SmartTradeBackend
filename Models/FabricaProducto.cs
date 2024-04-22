using SamartTradeBackend.Models.Productos;
using SmartTradeBackend.Services;

namespace SmartTradeBackend.Models
{
    internal class FabricaProducto
    {
        private readonly ServicioBD bd;
        public FabricaProducto(ServicioBD servicio)
        {
            bd = servicio;
        }
        public void crearProducto(string tipo, Producto p)
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
                Comida nuevoProducto = new Comida(idprod);
                bd.Insertar(nuevoProducto);
            }
            if (tipo == "electronica")
            {
                int idprod = bd.Insertar(p);
                Electronica nuevoProducto = new Electronica(idprod);
                bd.Insertar(nuevoProducto);
            }
        }

        //public FabricaProducto() { }
    }
}
