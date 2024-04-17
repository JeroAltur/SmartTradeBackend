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
            //_conexion.CreateTable<Valoracion>();
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

        public List<T> TodoOrdenado<T, U>(string orderByColumn) where T : new()
        {
            return _conexion.Table<T>().OrderBy<T, U>(x => (U)x.GetType().GetProperty(orderByColumn).GetValue(x)).ToList();
        }

        public void BorrarTodo()
        {
            Limpiar<Producto>();
            Limpiar<Comida>();
            Limpiar<Ropa>();
            Limpiar<Electronica>();
        }

        public T BuscarPorID<T>(int id) where T : class, new()
        {
            return _conexion.Find<T>(id);
        }

        public void Actualizar<T>(T entity) where T : class
        {
            _conexion.Update(entity);
        }

        public void Dispose()
        {
            _conexion.Close();
            _conexion.Dispose();
        }
    }
}