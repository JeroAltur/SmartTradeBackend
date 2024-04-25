using SmartTradeBackend.Services;
using Dapper;

namespace SamartTradeBackend.Models.Productos
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double precio { get; set; }
        public string imagenes { get; set; }
        public double HuellaAmbiental { get; set; }
        public Valoracion valoracion { get; set; }
        public int ventas { get; set; }

        public Producto()
        {
            imagenes = "";
            HuellaAmbiental = 0;
            Random rnd = new Random();
            ventas = rnd.Next(0, 9999);
            valoracion = new Valoracion();
        }

        public Producto(string nombre, string descripcion, double precio, string imagenes, double huellaAmbiental) : this()
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
            this.imagenes = imagenes;
            HuellaAmbiental = huellaAmbiental;
        }

        public void venta(ServicioBD servicio)
        {
            ventas++;
        }

        public void ValoracionNueva(double v)
        {
            valoracion.valoracionNueva(v);
        }
    }
}
