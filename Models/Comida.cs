using SmartTradeBackend.Services;
using Dapper;

namespace SmartTradeBackend.Models
{
    public class Comida
    {
        public int Id { get; set; }
        public int id_Prod { get; set; }

        public Comida() { }

        public Comida(Producto p)
        {
            this.id_Prod = p.idProducto;
        }
    }
}
