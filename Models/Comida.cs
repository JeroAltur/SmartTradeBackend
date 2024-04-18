using SQLite;

namespace SmartTradeBackend.Models
{
    internal class Comida
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int id_Prod { get; set; }

        public Comida() { }

        public Comida(Producto p)
        {
            this.id_Prod = p.idProducto;
        }
    }
}
