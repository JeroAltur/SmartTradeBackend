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
        public string AgregarUsuario(int dni, string nombre, string correo, string direccion, string contraseña, string tipo)
        {
            if (tienda.Clientes.Any(cliente => cliente.DNI == dni))
            {
                return "Error: Ya existe un usuario con el mismo DNI.";
            }

            if (tienda.Clientes.Any(cliente => cliente.correo == correo))
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
            // Buscar en la lista de clientes
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

            return null;
        }

        public Usuario BuscarUsuarioPorId(int idUsuario)
        {
            // Buscar en la lista de clientes
            Usuario cliente = tienda.Clientes.FirstOrDefault(c => c.DNI == idUsuario);
            if (cliente != null)
            {
                return cliente;
            }

            // Buscar en la lista de técnicos
            Usuario tecnico = tienda.Tecnicos.FirstOrDefault(t => t.DNI == idUsuario);
            if (tecnico != null)
            {
                return tecnico;
            }

            // Buscar en la lista de vendedores
            Usuario vendedor = tienda.Vendedores.FirstOrDefault(v => v.DNI == idUsuario);
            if (vendedor != null)
            {
                return vendedor;
            }

            // Si no se encuentra el usuario, devolver null
            return null;
        }

        //ListaDeseos
        public void AñadirListaDeseos(int dni, int prod)
        {
            // Buscar el usuario por su ID
            Usuario usuario = BuscarUsuarioPorId(dni);
            if (usuario != null)
            {
                // Buscar el producto por su ID
                Producto producto = tienda.BuscarProductoPorId(prod);
                if (producto != null)
                {
                    // Agregar el producto a la lista de deseos del usuario
                    usuario.listaDeseos.AgregarProducto(producto);
                }
                else
                {
                    
                }
            }
            else
            {
                
            }
        }

        public void EliminarListaDeseos(int dni, int prod)
        {
            // Buscar el usuario por su ID
            Usuario usuario = BuscarUsuarioPorId(dni);
            if (usuario != null)
            {
                // Buscar el producto por su ID
                Producto producto = tienda.BuscarProductoPorId(prod);
                if (producto != null)
                {
                    // Eliminar el producto de la lista de deseos del usuario
                    usuario.listaDeseos.EliminarProducto(producto);
                }
                else
                {
                    
                }
            }
            else
            {
                
            }
        }

        private void ActualizarUsuarioEnTienda(Usuario usuarioActualizado)
        {
            // Verificar el tipo de usuario y actualizar la lista correspondiente en la tienda

            if (usuarioActualizado is Cliente)
            {
                Cliente clienteActualizado = usuarioActualizado as Cliente;
                Cliente clienteEnTienda = tienda.Clientes.FirstOrDefault(c => c.DNI == clienteActualizado.DNI);
                if (clienteEnTienda != null)
                {
                    // Actualizar la información del cliente en la tienda
                    tienda.Clientes.Remove(clienteEnTienda);
                    tienda.Clientes.Add(clienteActualizado);
                }
                else
                {
                    // Manejar el caso cuando no se encuentra el cliente
                }
            }
            else if (usuarioActualizado is Tecnico)
            {
                Tecnico tecnicoActualizado = usuarioActualizado as Tecnico;
                Tecnico tecnicoEnTienda = tienda.Tecnicos.FirstOrDefault(t => t.DNI == tecnicoActualizado.DNI);
                if (tecnicoEnTienda != null)
                {
                    // Actualizar la información del técnico en la tienda
                    tienda.Tecnicos.Remove(tecnicoEnTienda);
                    tienda.Tecnicos.Add(tecnicoActualizado);
                }
                else
                {
                    // Manejar el caso cuando no se encuentra el técnico
                }
            }
            else if (usuarioActualizado is Vendedor)
            {
                Vendedor vendedorActualizado = usuarioActualizado as Vendedor;
                Vendedor vendedorEnTienda = tienda.Vendedores.FirstOrDefault(v => v.DNI == vendedorActualizado.DNI);
                if (vendedorEnTienda != null)
                {
                    // Actualizar la información del vendedor en la tienda
                    tienda.Vendedores.Remove(vendedorEnTienda);
                    tienda.Vendedores.Add(vendedorActualizado);
                }
                else
                {
                    // Manejar el caso cuando no se encuentra el vendedor
                }
            }
        }




    }
}
