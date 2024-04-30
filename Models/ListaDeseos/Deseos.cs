using SamartTradeBackend.Models.Productos;
using SmartTradeBackend.Services;

namespace SamartTradeBackend.Models.ListaDeseos
{
    public class Deseos
    {
        public int idDeseos { get; set; }
        public List<Producto> productos { get; set; }
        public Deseos() 
        { 
            productos = new List<Producto>();
        }


        public void AgregarProducto(Producto producto)
        {
            productos.Add(producto);
        }

        public void EliminarProducto(Producto producto)
        {
            productos.Remove(producto);
        }
    }
}
