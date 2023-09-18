using System;
using MySql.Data.MySqlClient;

namespace ConsoleAppVentasMuebles
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user=root;password=;database=ventasm;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            int numEmpleados;
            string cedula, nombre, puntoVenta;
            double venta1Mes, venta2Mes, venta3Mes;

            Console.Write("Digite el número de Empleados: ");
            numEmpleados = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < numEmpleados; i++)
            {
                Console.Write("\nDigite la Cédula del Empleado: ");
                cedula = Console.ReadLine();
                Console.Write("\nDigite el Nombre del Empleado: ");
                nombre = Console.ReadLine();
                Console.Write("\nDigite el Punto de Venta del Empleado: ");
                puntoVenta = Console.ReadLine();
                Console.Write("\nDigite las ventas en el Primer Mes del Empleado: ");
                venta1Mes = Int32.Parse(Console.ReadLine());
                Console.Write("\nDigite las ventas en el Segundo Mes del Empleado: ");
                venta2Mes = Int32.Parse(Console.ReadLine());
                Console.Write("\nDigite las ventas en el Tercer Mes del Empleado: ");
                venta3Mes = Int32.Parse(Console.ReadLine());

                string insertQuery = $"INSERT INTO empleados (cedula, nombre, punto_venta, venta_1mes, venta_2mes, venta_3mes) VALUES ('{cedula}', '{nombre}', '{puntoVenta}', {venta1Mes}, {venta2Mes}, {venta3Mes})";

                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                connection.Open();
                insertCommand.ExecuteNonQuery();
                connection.Close();
            }

            // Menú
            while (true)
            {
                Console.WriteLine("\nMENÚ DE OPCIONES\n");
                Console.WriteLine("1. Actualizar Empleado");
                Console.WriteLine("2. Eliminar Empleado");
                Console.WriteLine("3. Listar Empleados");
                Console.WriteLine("4. Calcular Promedio de Ventas Totales");
                Console.WriteLine("5. Salir");
                Console.Write("\nSeleccione una Opción: ");

                int opcion = Int32.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Digite la Cédula del Empleado a Actualizar: ");
                        string cedulaActualizar = Console.ReadLine();
                        Console.Write("\nNuevo Nombre: ");
                        string nuevoNombre = Console.ReadLine();
                        Console.Write("\nNuevo Punto de Venta: ");
                        string nuevoPuntoV = Console.ReadLine();
                        Console.Write("\nNueva Venta 1 Mes: ");
                        double nuevaV1Mes = Double.Parse(Console.ReadLine());
                        Console.Write("\nNueva Venta 2 Mes: ");
                        double nuevaV2Mes = Double.Parse(Console.ReadLine());
                        Console.Write("\nNueva Venta 3 Mes: ");
                        double nuevaV3Mes = Double.Parse(Console.ReadLine());
                        ActualizarEmpleado(cedulaActualizar, nuevoNombre, nuevoPuntoV, nuevaV1Mes, nuevaV2Mes, nuevaV3Mes, connection);
                        break;

                    case 2:
                        Console.Clear();
                        Console.Write("Digite la Cédula del Empleado a Eliminar: ");
                        string cedulaEliminar = Console.ReadLine();
                        EliminarEmpleado(cedulaEliminar, connection);
                        break;

                    case 3:
                        Console.Clear();
                        ListarEmpleados(connection);
                        break;

                    case 4:
                        Console.Clear();
                        CalcularPromedioVentas(connection);
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("\nSaliendo del Programa...");
                        return;

                    default:
                        Console.WriteLine("\nOpción NO Válida. Seleccione una Opción Válida del Menú");
                        break;
                }
            }

        }
        // Función para Actualizar un Empleado por su Cédula
        static void ActualizarEmpleado(string cedula, string nuevoNombre, string nuevoPuntoV, double nuevaV1Mes, double nuevaV2Mes, double nuevaV3Mes, MySqlConnection connection)
        {
            string updateQuery = $"UPDATE empleados SET nombre = '{nuevoNombre}', punto_venta = '{nuevoPuntoV}', venta_1mes = {nuevaV1Mes}, venta_2mes = {nuevaV2Mes}, venta_3mes = {nuevaV3Mes} WHERE cedula = '{cedula}'";

            MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
            connection.Open();
            updateCommand.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("\nEmpleado Actualizado Exitosamente");
        }

        // Función para Eliminar un Empleado por su Cédula
        static void EliminarEmpleado(string cedula, MySqlConnection connection)
        {
            string deleteQuery = $"DELETE FROM empleados WHERE cedula = '{cedula}'";

            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
            connection.Open();
            deleteCommand.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("\nEmpleado Eliminado Exitosamente.");
        }

        // Función para Listar los Empleados
        static void ListarEmpleados(MySqlConnection connection)
        {
            string selectQuery = "SELECT cedula, nombre, punto_venta, venta_1mes, venta_2mes, venta_3mes FROM empleados";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            connection.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("Lista de Empleados");
                while (reader.Read())
                {
                    string cedula = reader.GetString("cedula");
                    string nombre = reader.GetString("nombre");
                    string puntoV = reader.GetString("punto_venta");
                    double venta1Mes = reader.GetDouble("venta_1mes");
                    double venta2Mes = reader.GetDouble("venta_2mes");
                    double venta3Mes = reader.GetDouble("venta_3mes");

                    Console.WriteLine($"\nCédula: {cedula}, Nombre: {nombre}, Punto de Venta: {puntoV}, \nVenta 1 Mes: {venta1Mes}, Venta 2 Mes: {venta2Mes}, Venta 3 Mes: {venta3Mes}");
                }
            }

            connection.Close();
        }

        static void CalcularPromedioVentas(MySqlConnection connection)
        {
            string selectQuery = "SELECT venta_1mes, venta_2mes, venta_3mes FROM empleados";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            connection.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                double PromedioVentas = 0;
                while (reader.Read())
                {
                    double venta1Mes = reader.GetDouble("venta_1mes");
                    double venta2Mes = reader.GetDouble("venta_2mes");
                    double venta3Mes = reader.GetDouble("venta_3mes");
                    PromedioVentas += (venta1Mes + venta2Mes + venta3Mes) / 3;
                }

                Console.WriteLine("El Promedio de Ventas Totales es: " + PromedioVentas);
            }

            connection.Close();
        }
    }
}