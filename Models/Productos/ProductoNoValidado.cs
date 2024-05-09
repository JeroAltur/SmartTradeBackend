using SamartTradeBackend.Models.Usuarios;

namespace SamartTradeBackend.Models.Productos
{
    public class ProductoNoValidado
    {
        public int idProductoN { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double precio { get; set; }
        public string imagenes { get; set; }
        public double HuellaAmbiental { get; set; }
        public string tipo { get; set; }
        public Usuario Vendedor { get; set; }

        public ProductoNoValidado()
        {
            imagenes = "";
            HuellaAmbiental = 0;
        }

        public ProductoNoValidado(string nombre, string descripcion, double precio, string imagenes, double huellaAmbiental, string tipo,Usuario vendedor) : this()
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
            this.imagenes = imagenes;
            this.HuellaAmbiental = huellaAmbiental;
            this.tipo = tipo;
            this.Vendedor = vendedor;
        }
    }
}
