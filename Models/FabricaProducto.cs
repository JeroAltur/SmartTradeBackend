using SamartTradeBackend.Models.Productos;
using SmartTradeBackend.Services;

namespace SmartTradeBackend.Models
{
    internal class FabricaProducto
    {
        public FabricaProducto()
        {
            
        }
        public void crearProducto(string tipo, Producto p)
        {
            if (tipo == "ropa")
            {
                Ropa nuevoProducto = new Ropa(p);
            }
            if (tipo == "comida")
            {
                Comida nuevoProducto = new Comida(p);
            }
            if (tipo == "electronica")
            {
                Electronica nuevoProducto = new Electronica(p);
            }
        }

        //public FabricaProducto() { }
    }
}
