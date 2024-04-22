namespace SamartTradeBackend.Models.Usuarios
{
    public class Vendedor
    {
        public int Id { get; set; }
        public int id_User { get; set; }

        public Vendedor() { }

        public Vendedor(int id_User)
        {
            this.id_User = id_User;
        }
    }
}
