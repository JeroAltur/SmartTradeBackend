using SmartTradeBackend.Services;
using Dapper;

namespace SmartTradeBackend.Models
{
    public class Ropa
    {
        public int Id { get; set; }
        public int id_Prod { get; set; }

        public Ropa() { }
        public Ropa(int idprod)
        {
            this.id_Prod = idprod;
        }
    }
}
