namespace SmartTradeBackend.Models
{
    internal class Ropa : Producto
    {

        public Ropa() { }
        public Ropa(Producto p)
        {
            this.idProducto = p.idProducto;
            this.nombre = p.nombre;
            this.descripcion = p.descripcion;
            this.precio = p.precio;
            this.imagenes = p.imagenes;
            this.HuellaAmbiental = p.HuellaAmbiental;
            this.id_valoracion = p.id_valoracion;
            this.valor = p.valor;
            this.ventas = p.ventas;
        }
    }
}
