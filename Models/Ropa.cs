using SQLite;

namespace SmartTradeBackend.Models
{
    internal class Ropa
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int id_Prod {  get; set; }

        public Ropa() { }
        public Ropa(Producto p)
        {
            this.id_Prod = p.idProducto;
        }
    }
}
