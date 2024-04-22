using SmartTradeBackend.Services;
using Dapper;

namespace SamartTradeBackend.Models.Productos
{
    public class Comida
    {
        public int Id { get; set; }
        public int id_Prod { get; set; }

        public Comida() { }

        public Comida(int idprod)
        {
            id_Prod = idprod;
        }
    }
}
