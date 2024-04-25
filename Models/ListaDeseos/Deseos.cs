using SamartTradeBackend.Models.Productos;
using SmartTradeBackend.Services;

namespace SamartTradeBackend.Models.ListaDeseos
{
    public class Deseos
    {
        public int idDeseos { get; set; }
        public List<Comida> comidas { get; set; }
        public List<Electronica> electronicas { get; set; }
        public List<Ropa> ropas { get; set; }
        public Deseos() { }


        public void AgregarProducto<T>(T producto) where T : Producto
        {
            if (producto is Comida)
            {
                comidas.Add(producto as Comida);
            }
            else if (producto is Electronica)
            {
                electronicas.Add(producto as Electronica);
            }
            else if (producto is Ropa)
            {
                ropas.Add(producto as Ropa);
            }
            else
            {
                throw new ArgumentException("Tipo de producto no compatible con el carro de compras.");
            }
        }

        public void EliminarProducto<T>(T producto) where T : Producto
        {
            if (producto is Comida)
            {
                comidas.Remove(producto as Comida);
            }
            else if (producto is Electronica)
            {
                electronicas.Remove(producto as Electronica);
            }
            else if (producto is Ropa)
            {
                ropas.Remove(producto as Ropa);
            }
            else
            {
                throw new ArgumentException("Tipo de producto no compatible con el carro de compras.");
            }
        }
    }
}
