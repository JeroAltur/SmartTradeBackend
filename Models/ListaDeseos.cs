using SmartTradeBackend.Services;
using SQLite;

namespace SmartTradeBackend.Models
{
    internal class ListaDeseos
    {
        [PrimaryKey, AutoIncrement]
        public int idDeseos {  get; set; }
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
            servicio.Actualizar<ListaDeseos>(this);

        }

        public void eliminarProducto(Producto p, ServicioBD servicio)
        {
            prod.Remove(p);
            servicio.Actualizar<ListaDeseos>(this);
        }
    }
}
