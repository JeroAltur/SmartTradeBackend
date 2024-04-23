namespace SamartTradeBackend.Models.CarroCompra
{
    public class ProductoCarroCompra
    {
        public int Id { get; set; }
        public int id_Compra { get; set; }
        public int id_Prod { get; set; }

        public ProductoCarroCompra() { }

        public ProductoCarroCompra(int id_Compra, int id_Prod)
        {
            this.id_Compra = id_Compra;
            this.id_Prod = id_Prod;
        }
    }
}
