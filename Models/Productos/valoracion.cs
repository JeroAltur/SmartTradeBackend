using SmartTradeBackend.Services;
using Dapper;

namespace SamartTradeBackend.Models.Productos
{
    public class Valoracion
    {
        public int idValoracion { get; set; }
        public double valoraciones { get; set; }
        public double total { get; set; }
        public double valor { get; set; }

        public Valoracion()
        {
            valor = 0;
            valoraciones = 0;
            total = 0;
        }

        public void valoracionNueva(double v)
        {
            valoraciones++;
            total += v;
            valor = total / valoraciones;
        }
    }
}

