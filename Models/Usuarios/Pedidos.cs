using SamartTradeBackend.Models.Productos;

namespace SamartTradeBackend.Models.Usuarios
{
    public class Pedidos
    {
        public int idPedidos { get; set; }
        public List<Producto> productos { get; set; }
        public double precio { get; set; }
        public DateTime llegada { get; set; }

        public Pedidos()
        {
            productos = new List<Producto>();
            precio = 0;
            llegada = DateTime.Now.AddDays(3);
        }

        public void AñadirProducto(Producto p)
        {
            precio += p.precio;
            productos.Add(p);
        }

    }
}
