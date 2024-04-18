using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Dapper;
using SmartTradeBackend.Models;
using System.Data;
using Dapper.Contrib.Extensions;

namespace SmartTradeBackend.Services
{
    public class ServicioBD : IDisposable
    {
        private readonly IDbConnection _conexion;

        public ServicioBD(IDbConnection conexion)
        {
            _conexion = conexion;
            _conexion.Open();

            // Crear tablas si no existen
            CrearTablas();
        }

        public void CrearTablas()
        {
            _conexion.Execute(@"
        CREATE TABLE IF NOT EXISTS Valoracions (
            idValoracion INT AUTO_INCREMENT PRIMARY KEY,
            valoraciones DOUBLE,
            total DOUBLE,
            valor DOUBLE,
            id_prod INT
        );
    ");

            // Luego, crear las otras tablas que hacen referencia a Valoracion
            _conexion.Execute(@"
        CREATE TABLE IF NOT EXISTS Productos (
            idProducto INT AUTO_INCREMENT PRIMARY KEY,
            nombre VARCHAR(255) NOT NULL,
            descripcion TEXT,
            precio DOUBLE,
            imagenes TEXT,
            HuellaAmbiental DOUBLE,
            id_valoracion INT,
            valor DOUBLE,
            ventas INT,
            FOREIGN KEY (id_valoracion) REFERENCES Valoracions(idValoracion)
        );
        CREATE TABLE IF NOT EXISTS Electronicas (
            Id INT AUTO_INCREMENT PRIMARY KEY,
            id_Prod INT,
            FOREIGN KEY (id_Prod) REFERENCES Productos(idProducto)
        );
        CREATE TABLE IF NOT EXISTS Comidas (
            Id INT AUTO_INCREMENT PRIMARY KEY,
            id_Prod INT,
            FOREIGN KEY (id_Prod) REFERENCES Productos(idProducto)
        );
        CREATE TABLE IF NOT EXISTS Ropas (
            Id INT AUTO_INCREMENT PRIMARY KEY,
            id_Prod INT,
            FOREIGN KEY (id_Prod) REFERENCES Productos(idProducto)
        );
    ");
        }

        public void Insertar<T>(T entity) where T : class
        {
            _conexion.Insert(entity);
        }

        public void Borrar<T>(T entity) where T : class
        {
            _conexion.Delete(entity);
        }

        public void Limpiar<T>() where T : class
        {
            _conexion.Execute($"DELETE FROM {typeof(T).Name};");
        }

        public List<T> Todo<T>() where T : new()
        {
            return _conexion.Query<T>($"SELECT * FROM {typeof(T).Name};").AsList();
        }

        public T BuscarPorId<T>(int id) where T : class, new()
        {
            return _conexion.QueryFirstOrDefault<T>($"SELECT * FROM {typeof(T).Name} WHERE Id = @Id;", new { Id = id });
        }

        public void BorrarTodo()
        {
            Limpiar<Producto>();
            Limpiar<Comida>();
            Limpiar<Ropa>();
            Limpiar<Electronica>();
            Limpiar<Valoracion>();
        }

        public void Actualizar<T>(T entity) where T : class
        {
            _conexion.Update(entity);
        }

        public void BorrarTablas()
        {
            _conexion.Execute("DROP TABLE IF EXISTS Electronicas;");
            _conexion.Execute("DROP TABLE IF EXISTS Comidas;");
            _conexion.Execute("DROP TABLE IF EXISTS Ropas;");
            
            _conexion.Execute("DROP TABLE IF EXISTS Productos;");

            _conexion.Execute("DROP TABLE IF EXISTS Valoracions;");
        }

        public void Dispose()
        {
            _conexion.Close();
            _conexion.Dispose();
        }
    }
}