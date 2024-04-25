using SamartTradeBackend.Models.CarroCompras;
using SamartTradeBackend.Models.Productos;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;

namespace SamartTradeBackend.Models
{
    public class Tienda
    {
        public List<Comida> Comida { get; set; }
        public List<Electronica> Electronica { get; set; }
        public List<Ropa> Ropa { get; set; }
        public List<Cliente> Clientes {  get; set; }
        public List<Tecnico> Tecnicos { get; set;}
        public List<Vendedor> Vendedores { get; set; }
        public int ultimoIdProducto { get; set; }
        public int ultimoIdValoracion { get; set; }
        public int ultimoIdUsuario { get; set; }
        public int ultimoIdCarrito { get; set; }
        public int ultimoIdDeseos { get; set; }

        public Tienda() { 
        
        }
    }
}
