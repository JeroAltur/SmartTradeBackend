namespace SamartTradeBackend.Models.Usuarios
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string contraseña { get; set; }

        public Usuario() { }

        public Usuario(string nombre, string correo, string direccion, string contraseña)
        {
            this.nombre = nombre;
            this.correo = correo;
            this.direccion = direccion;
            this.contraseña = contraseña;
        }
    }
}
