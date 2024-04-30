using SamartTradeBackend.Models.Productos;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;

namespace SamartTradeBackend.Models.CarroCompras
{
    public class CarroCompra
    {
        public int idCarro { get; set; }
        public List<Producto> productos { get; set; }
        public CarroCompra() { productos = new List<Producto>(); }

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
