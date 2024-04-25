﻿using SamartTradeBackend.Models.CarroCompras;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;
using SmartTradeBackend.Services;

namespace SamartTradeBackend.Models.Usuarios
{
    public class Tecnico: Usuario
    {

        public Tecnico() { }

        public Tecnico(Usuario u)
        {
            idUsuario = u.idUsuario;
            nombre = u.nombre;
            correo = u.correo;
            direccion = u.direccion;
            contraseña = u.contraseña;
            carrito = u.carrito;
            listaDeseos = u.listaDeseos;
        }
    }
}
