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

            if (tipo == "ropa")
            {
                Ropa nuevoProducto = null;
                nuevoProducto = new Ropa(p);
                bd.Insertar(nuevoProducto);
            }
            if (tipo == "comida")
            {
                Comida nuevoProducto = null;
                nuevoProducto = new Comida(p);
                bd.Insertar(nuevoProducto);
            }
            if (tipo == "electronica")
            {
                Electronica nuevoProducto = null;
                nuevoProducto = new Electronica(p);
                bd.Insertar(nuevoProducto);
            }
        }

        public FabricaProducto() { }
    }
}
