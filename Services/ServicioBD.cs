using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using Dapper.Contrib.Extensions;
using SamartTradeBackend.Models.Productos;

namespace SmartTradeBackend.Services
{
    public class ServicioBD
    {
        private readonly MySqlConnection _conexion;

        public ServicioBD(MySqlConnection conexion)
        {
            _conexion = conexion;
            conexion.Open();
        }

        public void CrearTablas()
        {
            using (var transaction = _conexion.BeginTransaction())
            {
                try
                {
                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS Valoracion (
                    idValoracion INT AUTO_INCREMENT PRIMARY KEY,
                    valoraciones DOUBLE,
                    total DOUBLE,
                    valor DOUBLE
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS Producto (
                    idProducto INT AUTO_INCREMENT PRIMARY KEY,
                    nombre VARCHAR(255) NOT NULL,
                    descripcion TEXT,
                    precio DOUBLE,
                    imagenes TEXT,
                    HuellaAmbiental DOUBLE,
                    id_valoracion INT,
                    valor DOUBLE,
                    ventas INT,
                    FOREIGN KEY (id_valoracion) REFERENCES Valoracion(idValoracion)
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS Electronica (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    id_Prod INT,
                    FOREIGN KEY (id_Prod) REFERENCES Producto(idProducto)
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS Comida (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    id_Prod INT,
                    FOREIGN KEY (id_Prod) REFERENCES Producto(idProducto)
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS Ropa (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    id_Prod INT,
                    FOREIGN KEY (id_Prod) REFERENCES Producto(idProducto)
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS Usuario (
                    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
                    nombre VARCHAR(255) NOT NULL,
                    correo VARCHAR(255) NOT NULL,
                    direccion TEXT,
                    contraseña VARCHAR(255) NOT NULL,
                    id_Deseos INT,
                    id_Carro INT,
                    FOREIGN KEY (id_Deseos) REFERENCES ListaDeseos(idDeseos),
                    FOREIGN KEY (id_Carro) REFERENCES CarroCompra(idCompra)
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS Cliente (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    id_User INT,
                    FOREIGN KEY (id_User) REFERENCES Usuario(idUsuario)
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS Tecnico (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    id_User INT,
                    FOREIGN KEY (id_User) REFERENCES Usuario(idUsuario)
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS Vendedor (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    id_User INT,
                    FOREIGN KEY (id_User) REFERENCES Usuario(idUsuario)
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS CarroCompra (
                    idCompra INT AUTO_INCREMENT PRIMARY KEY
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS ProductoCarroCompra (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    id_Compra INT,
                    id_Prod INT,
                    FOREIGN KEY (id_Compra) REFERENCES CarroCompra(idCompra),
                    FOREIGN KEY (id_Prod) REFERENCES Producto(idProducto)
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS ListaDeseos (
                    idDeseos INT AUTO_INCREMENT PRIMARY KEY
                );
            ");

                    _conexion.Execute(@"
                CREATE TABLE IF NOT EXISTS ProductoListaDeseos (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    id_Deseos INT,
                    id_Prod INT,
                    FOREIGN KEY (id_Deseos) REFERENCES ListaDeseos(idDeseos),
                    FOREIGN KEY (id_Prod) REFERENCES Producto(idProducto)
                );
            ");

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Manejar la excepción aquí
                }
            }
        }

        public int Insertar<T>(T entity) where T : class
        {
            var tableName = typeof(T).Name;
            var properties = typeof(T).GetProperties();
            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var valuePlaceholders = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            var query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({valuePlaceholders}); SELECT LAST_INSERT_ID()";
            return _conexion.ExecuteScalar<int>(query, entity);
        }

        public void Borrar<T>(T entity, string id) where T : class
        {
            var tableName = typeof(T).Name;
            var primaryKey = id;
            var primaryKeyValue = entity.GetType().GetProperty(primaryKey)?.GetValue(entity);

            if (primaryKeyValue != null)
            {
                var query = $"DELETE FROM {tableName} WHERE {primaryKey} = @PrimaryKeyValue";
                ExecuteNonQuery(query, new { PrimaryKeyValue = primaryKeyValue });
            }
            else
            {
                System.Console.WriteLine("no hay clave primaria");
            }
        }

        public void Limpiar<T>() where T : class
        {
            var tableName = typeof(T).Name;
            var query = $"DELETE FROM {tableName}";
            ExecuteNonQuery(query);
        }

        public List<T> Todo<T>() where T : new()
        {
            var query = $"SELECT * FROM {typeof(T).Name}";
            return ExecuteQuery<T>(query);
        }

        public List<T> TodoOrdenado<T, U>(string orderByColumn) where T : new()
        {
            var query = $"SELECT * FROM {typeof(T).Name} ORDER BY {orderByColumn}";
            return ExecuteQuery<T>(query);
        }

        public void BorrarTodo()
        {
            try
            {
                // Desactivar las restricciones de clave externa
                ExecuteNonQuery("SET FOREIGN_KEY_CHECKS = 0");

                // Borrar los datos de todas las tablas
                Limpiar<Producto>();
                Limpiar<Comida>();
                Limpiar<Ropa>();
                Limpiar<Electronica>();
                Limpiar<Valoracion>();

                // Volver a activar las restricciones de clave externa
                ExecuteNonQuery("SET FOREIGN_KEY_CHECKS = 1");
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí
                Console.WriteLine("Error al borrar todos los datos: " + ex.Message);
            }
        }

        public void BorrarTablas()
        {
            using (var transaction = _conexion.BeginTransaction())
            {
                try
                {
                    _conexion.Execute(@"
                DROP TABLE IF EXISTS ProductoListaDeseos;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS ListaDeseos;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS ProductoCarroCompra;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS CarroCompra;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS Vendedor;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS Tecnico;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS Cliente;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS Usuario;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS Ropa;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS Comida;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS Electronica;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS Producto;
            ");

                    _conexion.Execute(@"
                DROP TABLE IF EXISTS Valoracion;
            ");

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Manejar la excepción aquí
                }
            }
        }

        public void Cerrar()
        {
            _conexion.Close();
        }

        private void ExecuteNonQuery(string query)
        {
            using var cmd = new MySqlCommand(query, _conexion);
            cmd.ExecuteNonQuery();
        }

        private List<T> ExecuteQuery<T>(string query) where T : new()
        {
            var result = new List<T>();
            using var cmd = new MySqlCommand(query, _conexion);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // Mapea los datos del lector al objeto T 
                var item = new T();
                MapDataReaderToEntity(reader, item);

                result.Add(item);
            }
            return result;
        }

        private void ExecuteNonQuery(string query, object parameters = null)
        {
            using var command = new MySqlCommand(query, _conexion);

            if (parameters != null)
            {
                foreach (var property in parameters.GetType().GetProperties())
                {
                    command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(parameters));
                }
            }

            command.ExecuteNonQuery();
        }

        private void MapDataReaderToEntity<T>(MySqlDataReader reader, T entity)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var ordinal = reader.GetOrdinal(property.Name);
                if (!reader.IsDBNull(ordinal))
                {
                    var value = reader.GetValue(ordinal);
                    if (value != DBNull.Value)
                    {
                        if (property.PropertyType.IsEnum)
                        {
                            // Manejar propiedad enum
                            property.SetValue(entity, Enum.ToObject(property.PropertyType, value));
                        }
                        else if (property.PropertyType == typeof(double))
                        {
                            // Convertir el valor a double si es necesario
                            if (value is float floatValue)
                            {
                                property.SetValue(entity, (double)floatValue);
                            }
                            else if (value is decimal decimalValue)
                            {
                                property.SetValue(entity, (double)decimalValue);
                            }
                            else
                            {
                                property.SetValue(entity, Convert.ToDouble(value));
                            }
                        }
                        else
                        {
                            // Asignar el valor a la propiedad
                            property.SetValue(entity, value);
                        }
                    }
                }
            }
        }

        private string GetSqlType(Type type)
        {
            if (type == typeof(int))
                return "INT";
            else if (type == typeof(string))
                return "VARCHAR(255)";
            else if (type == typeof(double))
                return "DOUBLE";
            else if (type == typeof(DateTime))
                return "DATETIME";
            else
                throw new NotSupportedException($"El tipo {type.Name} no es compatible con SQL.");
        }

        public T BuscarPorIdProducto<T>(int id) where T : new()
        {
            var tableName = typeof(T).Name;
            var primaryKey = "idProducto"; // Cambia esto al nombre de tu clave primaria
            var query = $"SELECT * FROM {tableName} WHERE {primaryKey} = @Id";

            using var cmd = new MySqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var item = new T();
                MapDataReaderToEntity(reader, item);
                return item;
            }
            else
            {
                return default;
            }
        }

        public T BuscarPorIdValoracion<T>(int id) where T : new()
        {
            var tableName = typeof(T).Name;
            var primaryKey = "idValoracion"; // Cambia esto al nombre de tu clave primaria
            var query = $"SELECT * FROM {tableName} WHERE {primaryKey} = @Id";

            using var cmd = new MySqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var item = new T();
                MapDataReaderToEntity(reader, item);
                return item;
            }
            else
            {
                return default;
            }
        }

        public void Actualizar<T>(T entity, string id) where T : class
        {
            var tableName = typeof(T).Name;
            var properties = typeof(T).GetProperties();
            var primaryKey = id;
            var primaryKeyValue = entity.GetType().GetProperty(primaryKey)?.GetValue(entity);

            if (primaryKeyValue != null)
            {
                var columnUpdates = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
                var query = $"UPDATE {tableName} SET {columnUpdates} WHERE {primaryKey} = @PrimaryKeyValue";

                var parameters = new Dictionary<string, object>();
                foreach (var property in properties)
                {
                    parameters[$"@{property.Name}"] = property.GetValue(entity);
                }
                parameters["@PrimaryKeyValue"] = primaryKeyValue;

                try
                {
                    ExecuteNonQuery(query, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al actualizar el registro: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("No se encontró la clave primaria para actualizar el objeto.");
            }
        }
    }
}