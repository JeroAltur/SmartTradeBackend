﻿using SamartTradeBackend.Models.CarroCompras;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;
using SmartTradeBackend.Services;

namespace SamartTradeBackend.Models.Usuarios
{
    public class Cliente: Usuario
    {

        public Cliente() { }

        public Cliente(Usuario u)
        {
            DNI = u.DNI;
            nombre = u.nombre;
            correo = u.correo;
            direccion = u.direccion;
            contraseña = u.contraseña;
            carrito = u.carrito;
            listaDeseos = u.listaDeseos;
            notificaciones = u.notificaciones;
            pedidos = u.pedidos;
        }
    }
}
