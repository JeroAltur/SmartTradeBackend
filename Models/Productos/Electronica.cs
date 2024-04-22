using SmartTradeBackend.Services;
using Dapper;

namespace SamartTradeBackend.Models.Productos
{
    public class Electronica
    {
        public int Id { get; set; }
        public int id_Prod { get; set; }

        public Electronica() { }
        public Electronica(int idprod)
        {
            id_Prod = idprod;
        }
    }
}
