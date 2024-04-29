using SamartTradeBackend.Models.CarroCompras;
using SamartTradeBackend.Models.Productos;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;
using SmartTradeBackend.Models;
using static Mysqlx.Crud.Order.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SamartTradeBackend.Models
{
    public class Tienda
    {
        public List<Comida> Comida { get; set; }
        public List<Electronica> Electronica { get; set; }
        public List<Ropa> Ropa { get; set; }
        public List<Cliente> Clientes {  get; set; }
        public List<Tecnico> Tecnicos { get; set;}
        public List<Vendedor> Vendedores { get; set; }
        public int ultimoIdProducto { get; set; }
        public int ultimoIdValoracion { get; set; }
        public int ultimoIdCarrito { get; set; }
        public int ultimoIdDeseos { get; set; }

        public Tienda() { 
            Comida = new List<Comida>();
            Electronica = new List<Electronica>();
            Ropa = new List<Ropa>();
            Clientes = new List<Cliente>();
            Tecnicos = new List<Tecnico>();
            Vendedores = new List<Vendedor>();

            ultimoIdProducto = 0;
            ultimoIdValoracion = 0;
            ultimoIdCarrito = 0;
            ultimoIdDeseos = 0;

            IniciarObjetos();
        
        }

        public Producto BuscarProductoPorId(int id)
        {
            // Buscar en la lista de Comida
            var comida = Comida.FirstOrDefault(p => p.idProducto == id);
            if (comida != null)
                return comida;

            // Buscar en la lista de Electronica
            var electronica = Electronica.FirstOrDefault(p => p.idProducto == id);
            if (electronica != null)
                return electronica;

            // Buscar en la lista de Ropa
            var ropa = Ropa.FirstOrDefault(p => p.idProducto == id);
            if (ropa != null)
                return ropa;

            // Si no se encuentra el producto, devolver null o lanzar una excepción según tu lógica
            // Aquí, por ejemplo, se devuelve null
            return null;
        }

        public void IniciarObjetos()
        {
            //Iniciar Productos
            FabricaProducto fp = new FabricaProducto();

            Producto p1 = new Producto("teclado", "teclado con pad numerico", 20, "../Resources/Imgages/teclado.png", 0);
            fp.crearProducto(p1, "electronica", this);


            Producto p2 = new Producto("Redmi15", "movil xiaomi de ultima generacion ", 300, "../Resources/Imgages/redmi15.png", 0);
            fp.crearProducto(p2, "electronica", this);


            Producto p3 = new Producto("Manzana roja", "manzana roja cultivada en España", 20, "../Resources/Imgages/manzanaroja.png", 0);
            fp.crearProducto(p3, "comida", this);


            Producto p4 = new Producto("Sudadera supreme", "sudadera de alta calidad", 20, "../Resources/Imgages/sudaderasupreme.png", 0);
            fp.crearProducto(p4, "ropa", this);


            Producto p5 = new Producto("Redmi15Pro", "xiaomi Redmi15 con mejoras en el rendimiento y almacenamiento", 20, "../Resources/Imgages/redmi15.png", 0);
            fp.crearProducto(p5, "electronica", this);


            Producto p6 = new Producto("Redmi14", "movil xiaomi de alta calidad", 275, "../Resources/Imgages/redmi14.png", 0);
            fp.crearProducto(p6, "electronica", this);


            Producto p7 = new Producto("Redmi13", "movil xiaomi de alta calidad", 250, "../Resources/Imgages/redmi13.png", 0);
            fp.crearProducto(p7, "electronica", this);


            Producto p8 = new Producto("Redmi12", "movil xiaomi de alta calidad", 225, "../Resources/Imgages/redmi12.png", 0);
            fp.crearProducto(p8, "electronica", this);


            Producto p9 = new Producto("Redmi11", "movil xiaomi de alta calidad", 200, "../Resources/Imgages/redmi11.png", 0);
            fp.crearProducto(p9, "electronica", this);


            Producto p10 = new Producto("Redmi10", "movil xiaomi de alta calidad", 150, "../Resources/Imgages/redmi11.png", 0);
            fp.crearProducto(p10, "electronica", this);


            Producto p11 = new Producto("Redmia3", "movil xiaomi de alta calidad", 100, "../Resources/Imgages/redmia3.png", 0);
            fp.crearProducto(p11, "electronica", this);


            Producto p12 = new Producto("Iphone15", "Movil iphone con cargador incluido", 999.99, "../Resources/Imgages/iphone.png", 0);
            fp.crearProducto(p12, "electronica", this);

            Producto p13 = new Producto("Gafas de Sol", "Gafas para protejerse les sol", 999.99, "../Resources/Imgages/gafassol.png", 0);
            fp.crearProducto(p13, "ropa", this);

            Producto p14 = new Producto("Disfraz Dinosaurio", "Disfraz de dinosaurio guay", 999.99, "../Resources/Imgages/disfrazdino.png", 0);
            fp.crearProducto(p14, "ropa", this);

            Producto p15 = new Producto("AXE", "Desodorante", 999.99, "../Resources/Imgages/axe.png", 0);
            fp.crearProducto(p15, "ropa", this);

            //Iniciar usuarios
            FabricaUsuario fu = new FabricaUsuario();

            Usuario cliente = new Usuario(73365328, "Pablo", "pablo@gmail.com", "Comunitat Valenciana, Valencis, C/Ausias March 21", "contaseña");
            fu.crearUsuario(cliente, "cliente", this);

            Usuario vendedor = new Usuario(73965328, "Pep", "pep@gmail.com", "Comunitat Valenciana, Valencis, C/Ausias March 26", "contaseña");
            fu.crearUsuario(vendedor, "vendedor", this);

            Usuario tecnico = new Usuario(73365312, "Paco", "paco@gmail.com", "Comunitat Valenciana, Valencis, C/Ausias March 33", "contaseña");
            fu.crearUsuario(tecnico, "tecnico", this);
        }
    }
}
