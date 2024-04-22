using SmartTradeBackend.Services;
using Dapper;

namespace SamartTradeBackend.Models.Productos
{
    public class Ropa
    {
        public int Id { get; set; }
        public int id_Prod { get; set; }

        public Ropa() { }
        public Ropa(int idprod)
        {
            id_Prod = idprod;
        }
    }
}
