namespace SamartTradeBackend.Models.Usuarios
{
    public class Cliente
    {
        public int Id { get; set; }
        public int id_User { get; set; }

        public Cliente() { }

        public Cliente(int id_User)
        {
            this.id_User = id_User;
        }
    }
}
