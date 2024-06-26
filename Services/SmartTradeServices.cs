﻿using SamartTradeBackend;
using SamartTradeBackend.Models;
using SamartTradeBackend.Models.CarroCompras;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Productos;
using SamartTradeBackend.Models.Usuarios;
using SmartTradeBackend.Models;
using System.IO.Pipelines;
using System.Net;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartTradeBackend.Services
{
    public class SmartTradeServices
    {
        private static SmartTradeServices _instance;
        private static readonly object _lock = new object();
        public Tienda tienda;
        ConversorDeTipos conversor = new ConversorDeTipos();

        private SmartTradeServices()
        {
            tienda = new Tienda();
        }

        public static SmartTradeServices Instance
        {
            get
            {
                // Doble comprobación para la creación de la instancia
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SmartTradeServices();
                        }
                    }
                }
                return _instance;
            }
        }

        //Productos
        public List<Producto> TodoProducto()
        {
            List<Producto> result = new List<Producto>();
            foreach(Comida p in tienda.Comida)
            {
                result.Add(p);
            }
            foreach (Ropa p in tienda.Ropa)
            {
                result.Add(p);
            }
            foreach (Electronica p in tienda.Electronica)
            {
                result.Add(p);
            }

            return result;
        }

        public List<ProductoNoValidado> TodoPorValidar()
        {
            return tienda.Productos;
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

        public List<Producto> Buscador(string valor)
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

        public string SolicitarProducto(string nombre, string descripcion, double precio, string imagenes, double huellaAmbiental, string tipo, int dni)
        {
            if (tipo != "ropa" && tipo != "comida" && tipo != "electronica")
            {
                return "Tipo de producto no válido.";
            }

            string imagen = Uri.UnescapeDataString(imagenes); ;

            Usuario vendedor = new Usuario();
            for (int i = 0; i < tienda.Vendedores.Count; i++)
            {
                if (tienda.Vendedores[i].DNI == dni)
                {
                    vendedor = tienda.Vendedores[i];
                }
            }

            ProductoNoValidado productoNoValidado = new ProductoNoValidado(nombre, descripcion, precio, imagen, huellaAmbiental, tipo, vendedor);
            productoNoValidado.idProductoN = ++tienda.ultimoIdProductoNoValidado;
            tienda.Productos.Add(productoNoValidado);

            CrearNotificacion(dni, "Producto solicitado", "Tu producto: " + nombre + ", ha sido enviado a solicitud de ventas.");

            return "Venta de producto solicitada correctamente";
        }

        public string AceptarProducto(int id)
        {
            ProductoNoValidado p = new ProductoNoValidado();
            for (int i = 0; i < tienda.Productos.Count; i++)
            {
                if (tienda.Productos[i].idProductoN == id)
                {
                    p = tienda.Productos[i];
                    tienda.Productos.Remove(p);
                }
            }

            Producto prod = AgregarProducto(p.nombre, p.descripcion, p.precio, p.imagenes, p.HuellaAmbiental, p.tipo);

            CrearNotificacion(p.Vendedor.DNI, "Producto aceptado", "Tu producto: " + p.nombre + ", ha sido aceptado para venta.");

            return "Producto agregado correctamente";
        }

        public Producto AgregarProducto(string name, string description, double price, string imagenes, double huella, string tipo)
        {
            // Comprobar tipo de producto
            if (tipo != "ropa" && tipo != "comida" && tipo != "electronica")
            {
                return null;
            }

            // Asignar imágenes predeterminadas si imagenes es null o vacío
            if (string.IsNullOrEmpty(imagenes))
            {
                if (tipo == "electronica") { imagenes = "../Resources/Imgages/electronica.png"; }
                else if (tipo == "comida") { imagenes = "../Resources/Imgages/comida.png"; }
                else if (tipo == "ropa") { imagenes = "../Resources/Imgages/ropa.png"; }
            }

            // Crear el producto
            Producto p = new Producto(name, description, price, imagenes, huella);
            FabricaProducto fabricaProducto = new FabricaProducto();
            p = fabricaProducto.crearProducto(p, tipo, tienda);

            return p;
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

        //Usuarios
        public string AgregarUsuario(int dni, string nombre, string correo, string direccion, string contraseña, string tipo)
        {
            //comprobar tipo
            if(tipo != "cliente" && tipo != "vendedor" && tipo != "tecnico")
            {
                return "Tipo de usuario no valido.";
            }

            //Comprobar usuarios
            if (tienda.Clientes.Any(cliente => cliente.DNI == dni) || tienda.Vendedores.Any(cliente => cliente.DNI == dni) || tienda.Tecnicos.Any(cliente => cliente.DNI == dni))
            {
                return "Error: Ya existe un usuario con el mismo DNI.";
            }

            if (tienda.Clientes.Any(cliente => cliente.correo == correo) || tienda.Vendedores.Any(cliente => cliente.correo == correo) || tienda.Tecnicos.Any(cliente => cliente.correo == correo))
            {
                return "Error: Ya existe un usuario con el mismo correo electrónico.";
            }


            Usuario usuario = new Usuario(dni, nombre, correo, direccion, contraseña);
            FabricaUsuario fabricaUsuario = new FabricaUsuario();
            fabricaUsuario.crearUsuario(usuario, tipo, tienda);
            CrearNotificacion(dni, "Nuevo Usuario", "Gracias por registrarte en nuestra aplicacion, esperamos que disfrute su experiencia.");
            return "Usuario creado con éxito.";
        }

        public Usuario? Loguearse(string correo, string contraseña)
        {
            for (int i = 0; i < tienda.Clientes.Count; i++)
            {
                if (tienda.Clientes[i].correo == correo)
                {
                    if (tienda.Clientes[i].contraseña == contraseña)
                    {
                        CrearNotificacion(tienda.Clientes[i].DNI, "Nuevo Inicio de Sesion", "Se ha iniciado sesion a las " + DateTime.Now);
                        return tienda.Clientes[i];
                    }
                }
            }
            for (int i = 0; i < tienda.Vendedores.Count; i++)
            {
                if (tienda.Vendedores[i].correo == correo)
                {
                    if(tienda.Vendedores[i].contraseña == contraseña)
                    {
                        CrearNotificacion(tienda.Vendedores[i].DNI, "Nuevo Inicio de Sesion", "Se ha iniciado sesion a las " + DateTime.Now);
                        return tienda.Vendedores[i];
                    }
                }
            }
            for (int i = 0; i < tienda.Tecnicos.Count; i++)
            {
                if (tienda.Tecnicos[i].correo == correo)
                {
                    if (tienda.Tecnicos[i].contraseña == contraseña)
                    {
                        CrearNotificacion(tienda.Tecnicos[i].DNI, "Nuevo Inicio de Sesion", "Se ha iniciado sesion a las " + DateTime.Now);
                        return tienda.Tecnicos[i];
                    }
                }
            }
            return null;
        }

        public string TipoUsuario(int dni)
        {
            for (int i = 0; i < tienda.Clientes.Count; i++)
            {
                if (tienda.Clientes[i].DNI == dni)
                {
                    return "cliente";
                }
            }
            for (int i = 0; i < tienda.Vendedores.Count; i++)
            {
                if (tienda.Vendedores[i].DNI == dni)
                {
                    return "vendedor";
                }
            }
            for (int i = 0; i < tienda.Tecnicos.Count; i++)
            {
                if (tienda.Tecnicos[i].DNI == dni)
                {
                    return "tecnico";
                }
            }
            return "Usuario no encontrado";
        }

        //ListaDeseos
        public string AñadirListaDeseos(int dni, int prod)
        {
            Producto producto = tienda.BuscarProductoPorId(prod);
            if (producto != null)
            {
                CrearNotificacion(dni, "Producto agregado a deseos", "Producto: " + producto.nombre + ", ha sido añadido a su lista de deseos.");
                for (int i = 0; i < tienda.Clientes.Count; i++)
                {
                    if (tienda.Clientes[i].DNI == dni)
                    {
                        tienda.Clientes[i].listaDeseos.AgregarProducto(producto);
                        return "Producto añadido correctamente.";
                    }
                }
                for (int i = 0; i < tienda.Vendedores.Count; i++)
                {
                    if (tienda.Vendedores[i].DNI == dni)
                    {
                    tienda. Vendedores[i].listaDeseos.AgregarProducto(producto);
                    return "Producto añadido correctamente.";
                    }
                }
                for (int i = 0; i < tienda.Tecnicos.Count; i++)
                {
                    if (tienda.Tecnicos[i].DNI == dni)
                    {
                        tienda.Tecnicos[i].listaDeseos.AgregarProducto(producto);
                        return "Producto añadido correctamente.";
                    }
                }
            }
            return "No se ha podido agragar el producto.";
        }

        public string EliminarListaDeseos(int dni, int prod)
        {
            Producto producto = tienda.BuscarProductoPorId(prod);
            if (producto != null)
            {
                CrearNotificacion(dni, "Producto eliminado de deseos", "Producto: " + producto.nombre + ", ha sido eliminado de su lista de deseos.");
                for (int i = 0; i < tienda.Clientes.Count; i++)
                {
                    if (tienda.Clientes[i].DNI == dni)
                    {
                        tienda.Clientes[i].listaDeseos.EliminarProducto(producto);
                        return "Producto eliminado correctamente.";
                    }
                }
                for (int i = 0; i < tienda.Vendedores.Count; i++)
                {
                    if (tienda.Vendedores[i].DNI == dni)
                    {
                        tienda.Vendedores[i].listaDeseos.EliminarProducto(producto);
                        return "Producto eliminado correctamente.";
                    }
                }
                for (int i = 0; i < tienda.Tecnicos.Count; i++)
                {
                    if (tienda.Tecnicos[i].DNI == dni)
                    {
                        tienda.Tecnicos[i].listaDeseos.EliminarProducto(producto);
                        return "Producto eliminado correctamente.";
                    }
                }
            }
            return "No se ha podido eliminar el producto.";
        }

        public Deseos Deseos(int dni)
        {
            for (int i = 0; i < tienda.Clientes.Count; i++)
            {
                if (tienda.Clientes[i].DNI == dni)
                {
                    return tienda.Clientes[i].listaDeseos;
                }
            }
            for (int i = 0; i < tienda.Vendedores.Count; i++)
            {
                if (tienda.Vendedores[i].DNI == dni)
                {
                    return tienda.Vendedores[i].listaDeseos;
                }
            }
            for (int i = 0; i < tienda.Tecnicos.Count; i++)
            {
                if (tienda.Tecnicos[i].DNI == dni)
                {
                    return tienda.Tecnicos[i].listaDeseos;
                }
            }
            return null;
        }

        //Carrito
        public string AñadirCarrito(int dni, int prod)
        {
            Producto producto = tienda.BuscarProductoPorId(prod);
            if (producto != null)
            {
                CrearNotificacion(dni, "Producto agregado al carrito", "Producto: " + producto.nombre + ", ha sido añadido a su carrito de la compra.");
                for (int i = 0; i < tienda.Clientes.Count; i++)
                {
                    if (tienda.Clientes[i].DNI == dni)
                    {
                        tienda.Clientes[i].carrito.AgregarProducto(producto);
                        return "Producto añadido correctamente.";
                    }
                }
                for (int i = 0; i < tienda.Vendedores.Count; i++)
                {
                    if (tienda.Vendedores[i].DNI == dni)
                    {
                        tienda.Vendedores[i].carrito.AgregarProducto(producto);
                        return "Producto añadido correctamente.";
                    }
                }
                for (int i = 0; i < tienda.Tecnicos.Count; i++)
                {
                    if (tienda.Tecnicos[i].DNI == dni)
                    {
                        tienda.Tecnicos[i].carrito.AgregarProducto(producto);
                        return "Producto añadido correctamente.";
                    }
                }
            }
            return "No se ha podido agragar el producto.";
        }

        public string EliminarCarrito(int dni, int prod)
        {
            Producto producto = tienda.BuscarProductoPorId(prod);
            if (producto != null)
            {
                CrearNotificacion(dni, "Producto eliminado de carrito", "Producto: " + producto.nombre + ", ha sido eliminado de su carrito de la compra.");
                for (int i = 0; i < tienda.Clientes.Count; i++)
                {
                    if (tienda.Clientes[i].DNI == dni)
                    {
                        tienda.Clientes[i].carrito.EliminarProducto(producto);
                        return "Producto eliminado correctamente.";
                    }
                }
                for (int i = 0; i < tienda.Vendedores.Count; i++)
                {
                    if (tienda.Vendedores[i].DNI == dni)
                    {
                        tienda.Vendedores[i].carrito.EliminarProducto(producto);
                        return "Producto eliminado correctamente.";
                    }
                }
                for (int i = 0; i < tienda.Tecnicos.Count; i++)
                {
                    if (tienda.Tecnicos[i].DNI == dni)
                    {
                        tienda.Tecnicos[i].carrito.EliminarProducto(producto);
                        return "Producto eliminado correctamente.";
                    }
                }
            }
            return "No se ha podido eliminar el producto.";
        }

        public CarroCompra Carrito(int dni)
        {
            for (int i = 0; i < tienda.Clientes.Count; i++)
            {
                if (tienda.Clientes[i].DNI == dni)
                {
                    return tienda.Clientes[i].carrito;
                }
            }
            for (int i = 0; i < tienda.Vendedores.Count; i++)
            {
                if (tienda.Vendedores[i].DNI == dni)
                {
                    return tienda.Vendedores[i].carrito;
                }
            }
            for (int i = 0; i < tienda.Tecnicos.Count; i++)
            {
                if (tienda.Tecnicos[i].DNI == dni)
                {
                    return tienda.Tecnicos[i].carrito;
                }
            }
            return null;
        }

        //Pedidos
        public string RealizarPedido(int dni)
        {
            Pedidos pedidos = new Pedidos();

            CrearNotificacion(dni, "Pedido realizado", "Su pedido ha sido realizado, y llegara en un plazo de 3 dias. Disfrute de su compra.");

            for (int i = 0; i < tienda.Clientes.Count; i++)
            {
                if (tienda.Clientes[i].DNI == dni)
                {
                    pedidos.idPedidos = ++tienda.ultimoIdPedido;
                    for (int j = 0; j < tienda.Clientes[i].carrito.productos.Count; j++)
                    {
                        pedidos.AñadirProducto(tienda.Clientes[i].carrito.productos[j]);
                    }
                    tienda.Clientes[i].carrito.productos.Clear();
                    tienda.Clientes[i].carrito.precio = 0;
                    tienda.Clientes[i].pedidos.Add(pedidos);
                    return "Pedido realizado con exito";
                }
            }
            for (int i = 0; i < tienda.Vendedores.Count; i++)
            {
                if (tienda.Vendedores[i].DNI == dni)
                {
                    pedidos.idPedidos = ++tienda.ultimoIdPedido;
                    for (int j = 0; j < tienda.Vendedores[i].carrito.productos.Count; j++)
                    {
                        pedidos.AñadirProducto(tienda.Vendedores[i].carrito.productos[j]);
                    }
                    tienda.Vendedores[i].carrito.productos.Clear();
                    tienda.Vendedores[i].carrito.precio = 0;
                    tienda.Vendedores[i].pedidos.Add(pedidos);
                    return "Pedido realizado con exito";
                }
            }
            for (int i = 0; i < tienda.Tecnicos.Count; i++)
            {
                if (tienda.Tecnicos[i].DNI == dni)
                {
                    pedidos.idPedidos = ++tienda.ultimoIdPedido;
                    for (int j = 0; j < tienda.Tecnicos[i].carrito.productos.Count; j++)
                    {
                        pedidos.AñadirProducto(tienda.Tecnicos[i].carrito.productos[j]);
                    }
                    tienda.Tecnicos[i].carrito.productos.Clear();
                    tienda.Tecnicos[i].carrito.precio = 0;
                    tienda.Tecnicos[i].pedidos.Add(pedidos);
                    return "Pedido realizado con exito";
                }
            }

            return "No se ha podido realizar el pedido";
        }

        public List<Pedidos> ListaPedidos(int dni)
        {
            for (int i = 0; i < tienda.Clientes.Count; i++)
            {
                if (tienda.Clientes[i].DNI == dni)
                {
                    return tienda.Clientes[i].pedidos;
                }
            }
            for (int i = 0; i < tienda.Vendedores.Count; i++)
            {
                if (tienda.Vendedores[i].DNI == dni)
                {
                    return tienda.Vendedores[i].pedidos;
                }
            }
            for (int i = 0; i < tienda.Tecnicos.Count; i++)
            {
                if (tienda.Tecnicos[i].DNI == dni)
                {
                    return tienda.Tecnicos[i].pedidos;
                }
            }
            return null;
        }

        //Notificacion
        public void CrearNotificacion(int dni, string titulo, string descripcion)
        {
            for (int i = 0; i < tienda.Clientes.Count; i++)
            {
                if (tienda.Clientes[i].DNI == dni)
                {
                    Notificaciones notificacion = new Notificaciones(titulo, descripcion);
                    notificacion.idNotificacion = ++tienda.ultimoIdNotificacion;
                    tienda.Clientes[i].notificaciones.Add(notificacion);
                }
            }
            for (int i = 0; i < tienda.Vendedores.Count; i++)
            {
                if (tienda.Vendedores[i].DNI == dni)
                {
                    Notificaciones notificacion = new Notificaciones(titulo, descripcion);
                    notificacion.idNotificacion = ++tienda.ultimoIdNotificacion;
                    tienda.Vendedores[i].notificaciones.Add(notificacion);
                }
            }
            for (int i = 0; i < tienda.Tecnicos.Count; i++)
            {
                if (tienda.Tecnicos[i].DNI == dni)
                {
                    Notificaciones notificacion = new Notificaciones(titulo, descripcion);
                    notificacion.idNotificacion = ++tienda.ultimoIdNotificacion;
                    tienda.Tecnicos[i].notificaciones.Add(notificacion);
                }
            }
        }

        public void LeerNotificacion(int dni, int id)
        {
            for (int i = 0; i < tienda.Clientes.Count; i++)
            {
                if (tienda.Clientes[i].DNI == dni)
                {
                    for (int j = 0; j < tienda.Clientes[i].notificaciones.Count; j++)
                    {
                        if (tienda.Clientes[i].notificaciones[j].idNotificacion == id)
                        {
                            tienda.Clientes[i].notificaciones[j].leido = true;
                        }
                    }
                }
            }
            for (int i = 0; i < tienda.Vendedores.Count; i++)
            {
                if (tienda.Vendedores[i].DNI == dni)
                {
                    for (int j = 0; j < tienda.Vendedores[i].notificaciones.Count; j++)
                    {
                        if (tienda.Vendedores[i].notificaciones[j].idNotificacion == id)
                        {
                            tienda.Vendedores[i].notificaciones[j].leido = true;
                        }
                    }
                }
            }
            for (int i = 0; i < tienda.Tecnicos.Count; i++)
            {
                if (tienda.Tecnicos[i].DNI == dni)
                {
                    for (int j = 0; j < tienda.Tecnicos[i].notificaciones.Count; j++)
                    {
                        if (tienda.Tecnicos[i].notificaciones[j].idNotificacion == id)
                        {
                            tienda.Tecnicos[i].notificaciones[j].leido = true;
                        }
                    }
                }
            }
        }

        public string BorrarNotificacion(int dni, int id)
        {
            for (int i = 0; i < tienda.Clientes.Count; i++)
            {
                if (tienda.Clientes[i].DNI == dni)
                {
                    for (int j = 0; j < tienda.Clientes[i].notificaciones.Count; j++)
                    {
                        if (tienda.Clientes[i].notificaciones[j].idNotificacion == id)
                        {
                            tienda.Clientes[i].notificaciones.Remove(tienda.Clientes[i].notificaciones[j]);
                            return "Notificacion borrada con exito.";
                        }
                    }
                }
            }
            for (int i = 0; i < tienda.Vendedores.Count; i++)
            {
                if (tienda.Vendedores[i].DNI == dni)
                {
                    for (int j = 0; j < tienda.Vendedores[i].notificaciones.Count; j++)
                    {
                        if (tienda.Vendedores[i].notificaciones[j].idNotificacion == id)
                        {
                            tienda.Vendedores[i].notificaciones.Remove(tienda.Vendedores[i].notificaciones[j]);
                            return "Notificacion borrada con exito.";
                        }
                    }
                }
            }
            for (int i = 0; i < tienda.Tecnicos.Count; i++)
            {
                if (tienda.Tecnicos[i].DNI == dni)
                {
                    for (int j = 0; j < tienda.Tecnicos[i].notificaciones.Count; j++)
                    {
                        if (tienda.Tecnicos[i].notificaciones[j].idNotificacion == id)
                        {
                            tienda.Tecnicos[i].notificaciones.Remove(tienda.Tecnicos[i].notificaciones[j]);
                            return "Notificacion borrada con exito.";
                        }
                    }
                }
            }
            return "No se ha podido borrar la notificacion.";
        }

        public List<Notificaciones> ListaNotificaciones(int dni)
        {
            for (int i = 0; i < tienda.Clientes.Count; i++)
            {
                if (tienda.Clientes[i].DNI == dni)
                {
                    return tienda.Clientes[i].notificaciones;
                }
            }
            for (int i = 0; i < tienda.Vendedores.Count; i++)
            {
                if (tienda.Vendedores[i].DNI == dni)
                {
                    return tienda.Vendedores[i].notificaciones;
                }
            }
            for (int i = 0; i < tienda.Tecnicos.Count; i++)
            {
                if (tienda.Tecnicos[i].DNI == dni)
                {
                    return tienda.Tecnicos[i].notificaciones;
                }
            }
            return null;
        }

        //Auxiliars
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

        //CosasProbar
        public List<Cliente>? TodoCliente()
        {
            List<Cliente>? lista = new List<Cliente>();
            lista = tienda.Clientes;
            return lista;
        }

        public List<Vendedor>? TodoVendedor()
        {
            List<Vendedor>? lista = new List<Vendedor>();
            lista = tienda.Vendedores;
            return lista;
        }

        public List<Tecnico>? TodoTecnico()
        {
            List<Tecnico>? lista = new List<Tecnico>();
            lista = tienda.Tecnicos;
            return lista;
        }

        public string CambiarProrcentajeBarra(string input) {
            if (input == null) { throw new ArgumentNullException(nameof(input)); } return input.Replace('%', '/'); 
        }

    }
}
