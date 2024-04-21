using SmartTradeBackend.Services;
using Dapper;

namespace SmartTradeBackend.Models
{
    public class Comida
    {
        public int Id { get; set; }
        public int id_Prod { get; set; }

        public Comida() { }

        public Comida(int idprod)
        {
            this.id_Prod = idprod;
        }
    }
}
