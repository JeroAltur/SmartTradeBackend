namespace SamartTradeBackend.Models.Usuarios
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string contraseña { get; set; }
        public int id_Deseos { get; set; }
        public int id_Carro { get; set; }

        public Usuario() { }

        public Usuario(string nombre, string correo, string direccion, string contraseña, int id_Deseos, int id_Carro)
        {
            this.nombre = nombre;
            this.correo = correo;
            this.direccion = direccion;
            this.contraseña = contraseña;
            this.id_Deseos = id_Deseos;
            this.id_Carro = id_Carro;
        }
    }
}
