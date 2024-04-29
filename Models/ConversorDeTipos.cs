using SamartTradeBackend.Models.Productos;
using SamartTradeBackend.Models.Usuarios;

namespace SamartTradeBackend.Models
{
    public class ConversorDeTipos
    {
        public ConversorDeTipos() { }

        public Comida toComida(Producto p) 
        {
            Comida toComida = new Comida(p);
            return toComida;
        }

        public Electronica toElectronica(Producto p)
        {
            Electronica toElectronica = new Electronica(p);
            return toElectronica;
        }

        public Ropa toRopa(Producto p)
        {
            Ropa toRopa = new Ropa(p);
            return toRopa;
        }
    }
}
