using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Services
{
    internal class InicializacionServicioBD
    {
        public InicializacionServicioBD() { }

        public static string GetDatabasePath()
        {
            string dbName = "smarttrade.db3"; // Nombre de la base de datos
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); // Ruta del directorio de datos local
            string dataFolderPath = Path.Combine(folderPath, "Data"); // Carpeta donde se almacenará la base de datos

            // Crea la carpeta si no existe
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }

            // Ruta completa de la base de datos
            string fullPath = Path.Combine(dataFolderPath, dbName);
            return fullPath;
        }
    }
}
