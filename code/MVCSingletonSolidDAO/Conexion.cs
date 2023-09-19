using System;
using MySql.Data.MySqlClient;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public sealed class Conexion : IDisposable
    {
        private static readonly string ConnectionString = "server=localhost;user=root;password=;database=ventasm;";
        private MySqlConnection _connection;

        private static Conexion _instance = new Conexion();
        public static Conexion Instance => _instance;

        private bool _disposed = false;

        private Conexion()
        {
            _connection = new MySqlConnection(ConnectionString);
        }

        public MySqlConnection AbrirConexion()
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                    Console.WriteLine("Conectado");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
                throw;
            }

            return _connection;
        }

        public void CerrarConexion()
        {
            if (_connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
                Console.WriteLine("Conexión cerrada");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_connection != null)
                    {
                        CerrarConexion();
                        _connection.Dispose();
                    }
                }

                _disposed = true;
            }
        }
    }
}
