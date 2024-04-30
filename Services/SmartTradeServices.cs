using SamartTradeBackend;
using SamartTradeBackend.Models;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Productos;
using SamartTradeBackend.Models.Usuarios;
using SmartTradeBackend.Models;
using System.IO.Pipelines;
using System.Resources;

namespace SmartTradeBackend.Services
{
    public class SmartTradeServices
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

        public string AgregarProducto(string name, string description, double price, string imagenes, double huella, string tipo)
        {
            //comprobar tipo
            if (tipo != "ropa" || tipo != "comida" || tipo != "electronica")
            {
                return "Tipo de producto no valido.";
            }

            if (imagenes == null)
            {
                if (tipo == "electronica") { imagenes = "../Resources/Imgages/electronica.png"; }
                if (tipo == "comida") { imagenes = "../Resources/Imgages/comida.png"; }
                if (tipo == "ropa") { imagenes = "../Resources/Imgages/ropa.png"; }
            }
            Producto p = new Producto(name, description, price, imagenes, huella);
            FabricaProducto fabricaProducto = new FabricaProducto();
            fabricaProducto.crearProducto(p, tipo, tienda);
            return "Producto creado correctamente";
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
        public string AgregarUsuario(int dni, string nombre, string correo, string direccion, string contraseña, string tipo)
        {
            //comprobar tipo
            if(tipo != "cliente" || tipo != "vendedor" || tipo != "tecnico")
            {
                return "Tipo de usuario no valido.";
            }

            //Comprobar clientes
            if (tienda.Clientes.Any(cliente => cliente.DNI == dni))
            {
                return "Error: Ya existe un usuario con el mismo DNI.";
            }

            if (tienda.Clientes.Any(cliente => cliente.correo == correo))
            {
                return "Error: Ya existe un usuario con el mismo correo electrónico.";
            }

            //Comprobar vendedores
            if (tienda.Vendedores.Any(cliente => cliente.DNI == dni))
            {
                return "Error: Ya existe un usuario con el mismo DNI.";
            }

            if (tienda.Vendedores.Any(cliente => cliente.correo == correo))
            {
                return "Error: Ya existe un usuario con el mismo correo electrónico.";
            }

            //Comprobar tecnicos
            if (tienda.Tecnicos.Any(cliente => cliente.DNI == dni))
            {
                return "Error: Ya existe un usuario con el mismo DNI.";
            }

            if (tienda.Tecnicos.Any(cliente => cliente.correo == correo))
            {
                return "Error: Ya existe un usuario con el mismo correo electrónico.";
            }

            Usuario usuario = new Usuario(dni, nombre, correo, direccion, contraseña);
            FabricaUsuario fabricaUsuario = new FabricaUsuario();
            fabricaUsuario.crearUsuario(usuario, tipo, tienda); 
            return "Usuario creado con éxito.";
        }

        public Usuario? Loguearse(string correo, string contraseña)
        {
            try
            {
                Cliente? cliente = tienda.Clientes.FirstOrDefault(c => c.correo == correo && c.contraseña == contraseña);
                if (cliente != null)
                {
                    return cliente;
                }

                // Buscar en la lista de técnicos
                Tecnico? tecnico = tienda.Tecnicos.FirstOrDefault(t => t.correo == correo && t.contraseña == contraseña);
                if (tecnico != null)
                {
                    return tecnico;
                }

                // Buscar en la lista de vendedores
                Vendedor? vendedor = tienda.Vendedores.FirstOrDefault(v => v.correo == correo && v.contraseña == contraseña);
                if (vendedor != null)
                {
                    return vendedor;
                }

                // Si no se encuentra ningún usuario, lanzar una excepción
                throw new Exception("Correo o contraseña incorrectos.");
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante el proceso de inicio de sesión
                throw new Exception("Ocurrió un error durante el inicio de sesión: " + ex.Message);
            }


        }

        //ListaDeseos
        public string AñadirListaDeseos(int dni, int prod)
        {
            Producto producto = tienda.BuscarProductoPorId(prod);
            if (producto != null)
            {
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

        //Carrito
        public string AñadirCarrito(int dni, int prod)
        {
            Producto producto = tienda.BuscarProductoPorId(prod);
            if (producto != null)
            {
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

    }
}
