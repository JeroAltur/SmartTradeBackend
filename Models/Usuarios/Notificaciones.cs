namespace SamartTradeBackend.Models.Usuarios
{
    public class Notificaciones
    {
        public int idNotificacion {  get; set; }
        public string titulo {  get; set; }
        public string descripcion { get; set;}
        public bool leido { get; set; }

        public Notificaciones() { leido = false; }

        public Notificaciones(string titulo, string descripcion)
        {
            this.titulo = titulo;
            this.descripcion = descripcion;
        }
    }
}
