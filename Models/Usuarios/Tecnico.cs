namespace SamartTradeBackend.Models.Usuarios
{
    public class Tecnico
    {
        public int Id { get; set; }
        public int id_User { get; set; }

        public Tecnico() { }

        public Tecnico(int id_User)
        {
            this.id_User = id_User;
        }
    }
}
