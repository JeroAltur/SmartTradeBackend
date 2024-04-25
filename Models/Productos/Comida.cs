﻿using SmartTradeBackend.Services;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SamartTradeBackend.Models.Productos
{
    public class Comida: Producto
    {

        public Comida() { }

        public Comida(Producto p)
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
