using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SamartTradeBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HolaMundo : ControllerBase
    {
        /*
        GET: Se utiliza para solicitar datos de un recurso específico o una colección de recursos. Es un método seguro, lo que significa que no debería cambiar el estado del servidor ni los datos del recurso.

        POST: Se utiliza para enviar datos al servidor para crear un nuevo recurso. Se utiliza comúnmente para enviar datos de formularios o enviar datos JSON para crear un nuevo registro en la base de datos.

        PATCH: Se utiliza para realizar una actualización parcial en un recurso existente. En lugar de enviar todos los datos del recurso, se envían solo los campos que se desean actualizar.

        PUT: Similar a PATCH, se utiliza para actualizar un recurso existente, pero se espera que el cliente envíe todos los datos del recurso, no solo los campos que se desean actualizar. Esto significa que PUT se usa para realizar actualizaciones completas de un recurso.

        DELETE: Se utiliza para eliminar un recurso específico. Después de que se realiza una solicitud DELETE, el recurso correspondiente debe ser eliminado del servidor.

        OPTIONS: Se utiliza para solicitar información sobre las opciones de comunicación disponibles para un recurso o el servidor en sí. Por ejemplo, puede usarse para obtener los métodos HTTP permitidos para un recurso o para obtener información sobre los encabezados admitidos.

        HEAD: Es similar a GET, pero el servidor no devuelve el cuerpo de la respuesta, solo los encabezados. Se utiliza comúnmente para verificar si un recurso está disponible y para obtener información sobre la respuesta sin descargar el cuerpo completo.

        TRACE: Se utiliza para solicitar al servidor que devuelva lo que recibe, esto se utiliza a menudo para la depuración y el diagnóstico.
        */

        [HttpGet(Name = "HolaMundo")]
        public string Get()
        {
            return "HolaMundo";
        }

        [HttpGet("GetSaludo/{nombre}")]
        public string Get(string nombre)
        {
            return "Hola " + nombre;
        }

        public class User()
        {
            public int id {  get; set; }
            public string Nombre { get; set; }
            public int edad {  get; set; }
        }

        [HttpGet("GetUsuario/{id}/{nombre}/{edad}")]
        public string Get(int id, string nombre, int edad)
        {
            User usuario = new User();
            usuario.id = id;
            usuario.Nombre = nombre;
            usuario.edad = edad;
            string respuesta = JsonConvert.SerializeObject(usuario);
            return respuesta;
        }

        [HttpPost("PostUser")]
        public string Post(User usuario)
        {
            return JsonConvert.SerializeObject(usuario);
        }

        [HttpPatch("UpdateUser")]
        public string Patch(User usuario)
        {
            return JsonConvert.SerializeObject(usuario);
        }
    }
}
