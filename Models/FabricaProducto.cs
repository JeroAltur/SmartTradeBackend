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

            Valoracion valoracion = new Valoracion(p);
            bd.Insertar(valoracion);
            p.id_valoracion = valoracion.idValoracion;

            if (tipo == "ropa")
            {
                Ropa nuevoProducto = new Ropa(p);
                bd.Insertar(nuevoProducto);
                bd.Insertar(p);
            }
            if (tipo == "comida")
            {
                Comida nuevoProducto = new Comida(p);
                bd.Insertar(nuevoProducto);
                bd.Insertar(p);
            }
            if (tipo == "electronica")
            {
                Electronica nuevoProducto = new Electronica(p);
                bd.Insertar(nuevoProducto);
                bd.Insertar(p);
            }
        }

        //public FabricaProducto() { }
    }
}
