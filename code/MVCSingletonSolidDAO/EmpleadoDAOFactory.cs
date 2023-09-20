using MySql.Data.MySqlClient;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class EmpleadoDAOFactory
    {
        public static IEmpleadoDao CrearEmpleadoDAO()
        {
            using (MySqlConnection connection = Conexion.Instance.AbrirConexion())
            {
                return new EmpleadoDaoImpl(connection);
            }
        }
    }
}