namespace SamartTradeBackend.Models.ListaDeseos
{
    public class ProductoListaDeseos
    {
        public int Id { get; set; }
        public int id_Deseos { get; set; }
        public int id_Prod {  get; set; }

        public ProductoListaDeseos() { }

        public ProductoListaDeseos(int id_Deseos, int id_Prod)
        {
            this.id_Deseos = id_Deseos;
            this.id_Prod = id_Prod;
        }
    }
}
