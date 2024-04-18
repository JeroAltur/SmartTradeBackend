using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SQLite;
using SmartTradeBackend.Models;

namespace SmartTradeBackend.Services
{
    public class ServicioBD : IDisposable
    {
        private readonly SQLiteConnection _conexion;

        public ServicioBD(string dbPath)
        {
            _conexion = new SQLiteConnection(dbPath);
            _conexion.CreateTable<Valoracion>();
            _conexion.CreateTable<Producto>();
            _conexion.CreateTable<Electronica>();
            _conexion.CreateTable<Comida>();
            _conexion.CreateTable<Ropa>();
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
            _conexion.DeleteAll<T>();
        }

        public List<T> Todo<T>() where T : new()
        {
            return _conexion.Table<T>().ToList();
        }

        public T BuscarPorId<T>(int id) where T : class, new()
        {
            return _conexion.Find<T>(id);
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
            _conexion.Execute("DROP TABLE IF EXISTS Valoracion;");
            _conexion.Execute("DROP TABLE IF EXISTS Producto;");
            _conexion.Execute("DROP TABLE IF EXISTS Electronica;");
            _conexion.Execute("DROP TABLE IF EXISTS Comida;");
            _conexion.Execute("DROP TABLE IF EXISTS Ropa;");
        }

        public void Dispose()
        {
            _conexion.Close();
            _conexion.Dispose();
        }
    }
}
