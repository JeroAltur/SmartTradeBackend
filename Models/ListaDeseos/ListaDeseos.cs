using SamartTradeBackend.Models.Productos;
using SmartTradeBackend.Services;
using SQLite;

namespace SamartTradeBackend.Models.ListaDeseos
{
    internal class ListaDeseos
    {
        [PrimaryKey, AutoIncrement]
        public int idDeseos { get; set; }
        public List<Producto> prod { get; set; }
        public ListaDeseos()
        {
            prod = new List<Producto>();
        }

        public ListaDeseos(List<Producto> lista)
        {
            prod = lista;
        }

        public void añadirProducto(Producto p, ServicioBD servicio)
        {
            prod.Add(p);
            servicio.Actualizar(this, "idListaDeseos");

        }

        public void eliminarProducto(Producto p, ServicioBD servicio)
        {
            prod.Remove(p);
            servicio.Actualizar(this, "idListaDeseos");
        }
    }
}
