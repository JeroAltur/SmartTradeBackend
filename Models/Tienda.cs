using SamartTradeBackend.Models.CarroCompras;
using SamartTradeBackend.Models.Productos;
using SamartTradeBackend.Models.ListaDeseos;
using SamartTradeBackend.Models.Usuarios;
using SmartTradeBackend.Models;
using static Mysqlx.Crud.Order.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;

namespace SamartTradeBackend.Models
{
    public class Tienda
    {
        public List<ProductoNoValidado> Productos { get; set; }
        public List<Comida> Comida { get; set; }
        public List<Electronica> Electronica { get; set; }
        public List<Ropa> Ropa { get; set; }
        public List<Cliente> Clientes {  get; set; }
        public List<Tecnico> Tecnicos { get; set;}
        public List<Vendedor> Vendedores { get; set; }
        public int ultimoIdProductoNoValidado { get; set; }
        public int ultimoIdProducto { get; set; }
        public int ultimoIdValoracion { get; set; }
        public int ultimoIdCarrito { get; set; }
        public int ultimoIdDeseos { get; set; }
        public int ultimoIdNotificacion { get; set; }

        public Tienda() { 
            Productos = new List<ProductoNoValidado>();
            Comida = new List<Comida>();
            Electronica = new List<Electronica>();
            Ropa = new List<Ropa>();
            Clientes = new List<Cliente>();
            Tecnicos = new List<Tecnico>();
            Vendedores = new List<Vendedor>();

            ultimoIdProductoNoValidado = 0;
            ultimoIdProducto = 0;
            ultimoIdValoracion = 0;
            ultimoIdCarrito = 0;
            ultimoIdDeseos = 0;
            ultimoIdNotificacion = 0;

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

            Producto p1 = new Producto("teclado", "teclado con pad numerico", 20, "https://cdn.pixabay.com/photo/2022/05/18/07/19/keyboard-7204650_1280.png", 0); p1.valoracion.valor = 3;
            fp.crearProducto(p1, "electronica", this);


            Producto p2 = new Producto("Redmi15", "movil xiaomi de ultima generacion ", 300, "https://i0.wp.com/www.smartprix.com/bytes/wp-content/uploads/2024/01/Upcoming-Xiaomi-15-Production-Starts-Soon.png?ssl=1", 0); p2.valoracion.valor = 4;
            fp.crearProducto(p2, "electronica", this);


            Producto p3 = new Producto("Manzana roja", "manzana roja cultivada en España", 20, "https://static.vecteezy.com/system/resources/previews/020/899/515/non_2x/red-apple-isolated-on-white-png.png", 0); p3.valoracion.valor = 4;
            fp.crearProducto(p3, "comida", this);


            Producto p4 = new Producto("Sudadera supreme", "sudadera de alta calidad", 20, "https://static.flexdog.es/flexdog-7/products/images/23dc5a1b-57d2-4ef5-921f-72c42c2f6095_instyle_ai.png?width=350", 0); p4.valoracion.valor = 4;
            fp.crearProducto(p4, "ropa", this);


            Producto p5 = new Producto("Redmi15Pro", "xiaomi Redmi15 con mejoras en el rendimiento y almacenamiento", 20, "https://i0.wp.com/www.smartprix.com/bytes/wp-content/uploads/2024/01/Upcoming-Xiaomi-15-Production-Starts-Soon.png?ssl=1", 0); p5.valoracion.valor = 5;
            fp.crearProducto(p5, "electronica", this);


            Producto p6 = new Producto("Redmi14", "movil xiaomi de alta calidad", 275, "https://i05.appmifile.com/340_item_es/22/02/2024/a2630b3d162675a858acb0ffb357df20!400x400!85.png", 0); p6.valoracion.valor = 4;
            fp.crearProducto(p6, "electronica", this);


            Producto p7 = new Producto("Redmi13", "movil xiaomi de alta calidad", 250, "https://i05.appmifile.com/6_item_fr/15/01/2024/c482d3d54ac307ca0405d09107349cdc!400x400!85.png", 0); p7.valoracion.valor = 4;
            fp.crearProducto(p7, "electronica", this);


            Producto p8 = new Producto("Redmi12", "movil xiaomi de alta calidad", 225, "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_106575658/fee_786_587_png", 0); p8.valoracion.valor = 3;
            fp.crearProducto(p8, "electronica", this);


            Producto p9 = new Producto("Redmi11", "movil xiaomi de alta calidad", 200, "https://cdn.andro4all.com/andro4all/2022/01/Xiaomi-Redmi-Note-11S.png", 0); p9.valoracion.valor = 3;
            fp.crearProducto(p9, "electronica", this);


            Producto p10 = new Producto("Redmi10", "movil xiaomi de alta calidad", 150, "https://i01.appmifile.com/v1/MI_18455B3E4DA706226CF7535A58E875F0267/pms_1614652750.39695368.png", 0); p10.valoracion.valor = 2;
            fp.crearProducto(p10, "electronica", this);


            Producto p11 = new Producto("RedmiA3", "movil xiaomi de alta calidad", 100, "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_137912889/fee_786_587_png", 0); p11.valoracion.valor = 4;
            fp.crearProducto(p11, "electronica", this);


            Producto p12 = new Producto("Iphone15", "Movil iphone con cargador incluido", 999.99, "https://i.pinimg.com/originals/7f/19/a9/7f19a90645fa6dbae991abe0704e0630.png", 0); p12.valoracion.valor = 4;
            fp.crearProducto(p12, "electronica", this);

            Producto p13 = new Producto("Gafas de Sol", "Gafas para protejerse les sol", 999.99, "https://www.atmospherainteriorismo.es/wp-content/uploads/2022/10/gafas-de-sol-png-307dhl.png", 0); p13.valoracion.valor = 4;
            fp.crearProducto(p13, "ropa", this);

            Producto p14 = new Producto("Disfraz Dinosaurio", "Disfraz de dinosaurio guay", 59.99, "https://i.pinimg.com/originals/ff/55/8a/ff558a0194b94982b5f44bcc0fd7c12d.png", 0); p14.valoracion.valor = 5;
            fp.crearProducto(p14, "ropa", this);

            Producto p15 = new Producto("AXE", "Desodorante", 99.99, "https://assets.unileversolutions.com/v1/794338.png", 0); p15.valoracion.valor = 1;
            fp.crearProducto(p15, "ropa", this);

            //Iniciar usuarios
            FabricaUsuario fu = new FabricaUsuario();

            Usuario cliente = new Usuario(73365328, "Pablo", "pablo@gmail.com", "Comunitat Valenciana, Valencis, C/Ausias March 21", "contaseña");
            fu.crearUsuario(cliente, "cliente", this);

            Usuario vendedor = new Usuario(73965328, "Pep", "pep@gmail.com", "Comunitat Valenciana, Valencis, C/Ausias March 26", "contaseña");
            ProductosPep();
            fu.crearUsuario(vendedor, "vendedor", this);

            Usuario tecnico = new Usuario(73365312, "Paco", "paco@gmail.com", "Comunitat Valenciana, Valencis, C/Ausias March 33", "contaseña");
            fu.crearUsuario(tecnico, "tecnico", this);
        }

        /*public void ProductosPep()
        {
            for (int i = 0; i < this.Clientes.Count; i++)
            {
                if (this.Clientes[i].DNI == dni)
                {
                    return "cliente";
                }
            }
            for (int i = 0; i < this.Vendedores.Count; i++)
            {
                if (this.Vendedores[i].DNI == dni)
                {
                    return "vendedor";
                }
            }
            for (int i = 0; i < this.Tecnicos.Count; i++)
            {
                if (this.Tecnicos[i].DNI == dni)
                {
                    return "tecnico";
                }
            }
        }*/
    }
}
