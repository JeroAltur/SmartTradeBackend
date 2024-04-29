using SamartTradeBackend;
using SamartTradeBackend.Models;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Productos;
using SmartTradeBackend.Models;
using System.IO.Pipelines;
using System.Resources;

namespace SmartTradeBackend.Services
{
    internal class SmartTradeServices
    {
        Tienda tienda;
        ConversorDeTipos conversor = new ConversorDeTipos();

        public SmartTradeServices()
        {
            tienda = new Tienda();
        }

        //Productos
        public List<Producto> TodoProducto()
        {
            List<Producto> result = [.. tienda.Comida, .. tienda.Electronica, .. tienda.Ropa];

            return result;
        }

        public List<Producto> Tendencias()
        {
            List<Producto> resultadoProvicional = TodoProducto();

            List<Producto> result = resultadoProvicional.OrderByDescending(p => p.ventas).Take(8).ToList();
            return result;
        }

        public List<Producto> MejorValorado()
        {
            List<Producto> resultadoProvicional = TodoProducto();

            List<Producto> result = resultadoProvicional.OrderByDescending(p => p.valoracion.valor).Take(8).ToList();
            return result;
        }

        public List<Producto> CompradosPorIronman()
        {
            List<Producto> resultadoProvicional = new List<Producto>();
            List<Producto> result = new List<Producto>();
            resultadoProvicional = Buscador("Iphone15");
            foreach (Producto p in resultadoProvicional)
            {
                result.Add(p);
            }
            resultadoProvicional = Buscador("Sudadera supreme");
            foreach (Producto p in resultadoProvicional)
            {
                result.Add(p);
            }
            resultadoProvicional = Buscador("teclado");
            foreach (Producto p in resultadoProvicional)
            {
                result.Add(p);
            }
            resultadoProvicional = Buscador("Manzana");
            foreach (Producto p in resultadoProvicional)
            {
                result.Add(p);
            }

            return result;
        }

        public List<Producto> Buscador(String valor)
        {
            List<Producto> resultadoProvicional = TodoProducto();
            List<Producto> result = new List<Producto>();

            foreach (Producto p in resultadoProvicional)
            {
                if (p.nombre.Contains(valor) || p.descripcion.Contains(valor))
                {
                    result.Add(p);
                }
            }

            return result;
        }

        public void AgregarProducto(string name, string description, double price, string imagenes, double huella, string tipo)
        {
            if (imagenes == null)
            {
                if (tipo == "electronica") { imagenes = "../Resources/Imgages/electronica.png"; }
                if (tipo == "comida") { imagenes = "../Resources/Imgages/comida.png"; }
                if (tipo == "ropa") { imagenes = "../Resources/Imgages/ropa.png"; }
            }
            Producto p = new Producto(name, description, price, imagenes, huella);
            FabricaProducto fabricaProducto = new FabricaProducto();
            fabricaProducto.crearProducto(p, tipo, tienda);
        }

        public string AgregarValoracion(int idProd, int v)
        {
            // Buscar el producto por su ID
            Producto producto = tienda.BuscarProductoPorId(idProd);

            // Verificar si se encontró el producto
            if (producto != null)
            {
                // Agregar la valoración al producto
                producto.ValoracionNueva(v);

                // Actualizar el producto dentro de la tienda
                ActualizarProductoEnTienda(producto);

                return "Valoracion agregada";
            }
            else
            {
                // Manejar el caso cuando no se encuentra el producto
                return "El producto con la ID " + idProd + " no fue encontrado en la tienda.";
            }
        }

        private void ActualizarProductoEnTienda(Producto productoActualizado)
        {
            
            // Buscar el producto dentro de la tienda
            var comida = tienda.Comida.FirstOrDefault(p => p.idProducto == productoActualizado.idProducto);
            if (comida != null)
            {
                var productoActualizar = conversor.toComida(productoActualizado);
                // Actualizar el producto dentro de la lista de Comida
                tienda.Comida.Remove(comida);
                tienda.Comida.Add(productoActualizar);
                return;
            }

            var electronica = tienda.Electronica.FirstOrDefault(p => p.idProducto == productoActualizado.idProducto);
            if (electronica != null)
            {
                var productoActualizar = conversor.toElectronica(productoActualizado);
                // Actualizar el producto dentro de la lista de Electronica
                tienda.Electronica.Remove(electronica);
                tienda.Electronica.Add(productoActualizar);
                return;
            }

            var ropa = tienda.Ropa.FirstOrDefault(p => p.idProducto == productoActualizado.idProducto);
            if (ropa != null)
            {
                var productoActualizar = conversor.toRopa(productoActualizado);
                // Actualizar el producto dentro de la lista de Ropa
                tienda.Ropa.Remove(ropa);
                tienda.Ropa.Add(productoActualizar);
                return;
            }
        }



        //Usuarios

        //ListaDeseos
        /*public List<Producto> ObtenerListaDeseos()
        {
            List<Producto> ProductosDeseados = new List<Producto>();
            List<ListaDeseos> listasDeseos = bd.Todo<ListaDeseos>();
            List<Producto> lista = listasDeseos[0].prod;

            foreach (Producto p in lista)
            {
                ProductosDeseados.Add(p);
            }

            return ProductosDeseados;
        }*/


        /*public void AñadirListaDeseos(ListaDeseos ld, Producto p)
        {
            ld.añadirProducto(p, bd);
        }

        public void EliminarListaDeseos(ListaDeseos ld, Producto p)
        {
            ld.eliminarProducto(p, bd);
        }*/
    }
}
