using SmartTradeBackend.Services;

namespace SmartTradeBackend.Models
{
    internal class FabricaProducto
    {
        private readonly ServicioBD bd;
        public FabricaProducto(ServicioBD servicio)
        {
            bd = servicio;
        }
        public void crearProducto(string tipo, Producto p)
        {

            Valoracion valoracion = new Valoracion(p);
            bd.Insertar(valoracion);
            int id = bd.IdValoracion(p.idProducto);
            Console.WriteLine(id);
            p.id_valoracion = id;

            if (tipo == "ropa")
            {
                Ropa nuevoProducto = new Ropa(p);
                bd.Insertar(p);
                bd.Insertar(nuevoProducto);
            }
            if (tipo == "comida")
            {
                Comida nuevoProducto = new Comida(p);
                bd.Insertar(p);
                bd.Insertar(nuevoProducto);
            }
            if (tipo == "electronica")
            {
                Electronica nuevoProducto = new Electronica(p);
                bd.Insertar(p);
                bd.Insertar(nuevoProducto);
            }
        }

        //public FabricaProducto() { }
    }
}
