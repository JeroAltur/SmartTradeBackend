using SQLite;

namespace SmartTradeBackend.Models
{
    internal class Electronica
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int id_Prod { get; set; }

        public Electronica() { }
        public Electronica(Producto p)
        {
            this.id_Prod = p.idProducto;
        }
    }
}
