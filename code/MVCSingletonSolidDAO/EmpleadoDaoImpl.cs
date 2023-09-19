using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class EmpleadoDaoImpl : IEmpleadoDao
    {
        private const string INSERT_QUERY = "INSERT INTO empleados (cedula, nombre, punto_venta, venta_1mes, venta_2mes, venta_3mes, fecha_nac) VALUES (@cedula, @nombre, @puntoV, @venta1Mes, @venta2Mes, @venta3Mes, @fechaNac)";
        private const string SELECT_ALL_QUERY = "SELECT * FROM empleados ORDER BY ID";
        private const string UPDATE_QUERY = "UPDATE empleados SET cedula=@cedula, nombre=@nombre, punto_venta=@puntoV, venta_1mes=@venta1Mes, venta_2mes=@venta2Mes, venta_3mes=@venta3Mes, fecha_nac=@fechaNac WHERE ID=@id";
        private const string DELETE_QUERY = "DELETE FROM empleados WHERE ID=@id";
        private const string SELECT_BY_ID_QUERY = "SELECT * FROM empleados WHERE id=@id";

        private readonly MySqlConnection _connection;

        public EmpleadoDaoImpl(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public bool ActualizarEmpleado(Empleado empleado)
        {
            bool actualizado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(UPDATE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@cedula", empleado.Cedula);
                    cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@puntoV", empleado.PuntoVenta);
                    cmd.Parameters.AddWithValue("@venta1Mes", empleado.Venta1Mes);
                    cmd.Parameters.AddWithValue("@venta2Mes", empleado.Venta2Mes);
                    cmd.Parameters.AddWithValue("@venta3Mes", empleado.Venta3Mes);
                    cmd.Parameters.AddWithValue("@fechaNac", empleado.FechaNac);
                    cmd.Parameters.AddWithValue("@id", empleado.Id);
                    cmd.ExecuteNonQuery();
                    actualizado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("\nError al actualizar el empleado", ex);
            }
            finally
            {
                _connection.Close();
            }

            return actualizado;
        }

        public bool EliminarEmpleado(int id)
        {
            bool eliminado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(DELETE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    eliminado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("\nError al eliminar el empleado", ex);
            }
            finally
            {
                _connection.Close();
            }

            return eliminado;
        }

        public Empleado ObtenerEmpleadoPorId(int id)
        {
            Empleado empleado = null;

            try
            {

                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_BY_ID_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            empleado = CrearEmpleadoDesdeDataReader(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("\nError al obtener el empleado por ID", ex);
            }
            finally
            {
                _connection.Close();
            }

            return empleado;
        }

        public List<Empleado> ObtenerEmpleados()
        {
            using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_QUERY, _connection))
            {
                try
                {
                    ProveState();

                    List<Empleado> listaEmpleados = new List<Empleado>();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empleado empleado = CrearEmpleadoDesdeDataReader(reader);
                            listaEmpleados.Add(empleado);
                        }
                    }

                    return listaEmpleados;
                }
                catch (MySqlException ex)
                {
                    throw new DAOException("\nError al obtener los empleados", ex);
                }
            }
        }

        private Empleado CrearEmpleadoDesdeDataReader(MySqlDataReader reader)
        {
            int id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
            string cedula = reader.GetString("cedula");
            string nombre = reader.GetString("nombre");
            string puntoV = reader.GetString("punto_venta");
            double venta1Mes = reader.GetDouble("venta_1mes");
            double venta2Mes = reader.GetDouble("venta_2mes");
            double venta3Mes = reader.GetDouble("venta_3mes");
            DateTime fechaNac = reader.GetDateTime("fecha_nac");
            return new Empleado(id, cedula, nombre, puntoV, venta1Mes, venta2Mes, venta3Mes, fechaNac);
        }

        public bool RegistrarEmpleado(Empleado empleado)
        {
            using (MySqlCommand cmd = new MySqlCommand(INSERT_QUERY, _connection))
            {
                try
                {
                    ProveState();

                    cmd.Parameters.AddWithValue("@cedula", empleado.Cedula);
                    cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@puntoV", empleado.PuntoVenta);
                    cmd.Parameters.AddWithValue("@venta1Mes", empleado.Venta1Mes);
                    cmd.Parameters.AddWithValue("@venta2Mes", empleado.Venta2Mes);
                    cmd.Parameters.AddWithValue("@venta3Mes", empleado.Venta3Mes);
                    cmd.Parameters.AddWithValue("@fechaNac", empleado.FechaNac);

                    cmd.ExecuteNonQuery();

                    empleado.Id = (int)cmd.LastInsertedId;

                    return true;
                }
                catch (MySqlException ex)
                {
                    throw new DAOException("\nError al registrar el empleado", ex);
                }
            }
        }

        private void ProveState()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
    }
}
