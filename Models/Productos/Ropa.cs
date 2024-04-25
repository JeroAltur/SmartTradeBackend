using SmartTradeBackend.Services;
using Dapper;

namespace SamartTradeBackend.Models.Productos
{
    public class Ropa: Producto
    {

        public Ropa() { }
        public Ropa(Producto p)
        {
            idProducto = p.idProducto;
            nombre = p.nombre;
            descripcion = p.descripcion;
            precio = p.precio;
            imagenes = p.imagenes;
            HuellaAmbiental = p.HuellaAmbiental;
            valoracion = p.valoracion;
            ventas = p.ventas;
        }
    }
}
