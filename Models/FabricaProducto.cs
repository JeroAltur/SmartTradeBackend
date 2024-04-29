using SamartTradeBackend.Models;
using SamartTradeBackend.Models.Productos;
using SmartTradeBackend.Services;

namespace SmartTradeBackend.Models
{
    internal class FabricaProducto
    {
        public FabricaProducto()
        {
            
        }
        public void crearProducto(Producto p, string tipo, Tienda t)
        {
            if (tipo == "ropa")
            {
                Ropa nuevoProducto = new Ropa(p);
                nuevoProducto.idProducto = ++t.ultimoIdProducto;
                nuevoProducto.valoracion.idValoracion = ++t.ultimoIdValoracion;
                t.Ropa.Add(nuevoProducto);
            }
            if (tipo == "comida")
            {
                Comida nuevoProducto = new Comida(p);
                nuevoProducto.idProducto = ++t.ultimoIdProducto;
                nuevoProducto.valoracion.idValoracion = ++t.ultimoIdValoracion;
                t.Comida.Add(nuevoProducto);
            }
            if (tipo == "electronica")
            {
                Electronica nuevoProducto = new Electronica(p);
                nuevoProducto.idProducto = ++t.ultimoIdProducto;
                nuevoProducto.valoracion.idValoracion = ++t.ultimoIdValoracion;
                t.Electronica.Add(nuevoProducto);
            }
        }

        //public FabricaProducto() { }
    }
}
