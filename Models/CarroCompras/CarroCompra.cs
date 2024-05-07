using SamartTradeBackend.Models.Productos;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;

namespace SamartTradeBackend.Models.CarroCompras
{
    public class CarroCompra
    {
        public int idCarro { get; set; }
        public List<Producto> productos { get; set; }
        public double precio { get; set; }
        public CarroCompra() 
        { 
            productos = new List<Producto>();
            precio = 0;
        }

        public void AgregarProducto(Producto producto)
        {
            precio += producto.precio;
            productos.Add(producto);
        }

        public void EliminarProducto(Producto producto)
        {
            precio -= producto.precio;
            productos.Remove(producto);
        }
    }
}
