using System;
using System.Text.RegularExpressions;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string action;
            int id;

            IEmpleadoDao dao = EmpleadoDAOFactory.CrearEmpleadoDAO();
            EmpleadoView vista = new EmpleadoView();
            EmpleadoService empleadoService = new EmpleadoService(dao);
            EmpleadoController controller = new EmpleadoController(empleadoService, vista);

            while (true)
            {
                Console.WriteLine("\n----------------------------------------------------------------------------------------------");
                Console.WriteLine("[L]istar | [R]egistrar | [A]ctualizar | [E]liminar | [C]alcular Promedio | [S]alir | [H]elp: ");
                action = Console.ReadLine()?.ToUpper();

                if (!string.IsNullOrEmpty(action))
                {
                    try
                    {
                        switch (action)
                        {
                            case "L":
                                controller.ListarEmpleados();
                                break;
                            case "R":
                                Empleado nuevoEmpleado = InputEmpleado();
                                controller.RegistrarEmpleado(nuevoEmpleado);
                                break;
                            case "A":
                                id = InputId();
                                Console.WriteLine("----------------------------------------------------------------------------------------------");
                                controller.VerEmpleado(id);
                                Console.WriteLine("\n----------------------------------------------------------------------------------------------");
                                Console.WriteLine("\nDATOS NUEVOS");
                                string nuevaCedula = InputStringNumero("\nIngrese el Nuevo Número de Cédula del Empleado: ");
                                string nuevoNombre = InputString("Ingrese el Nuevo Nombre del Empleado: ");
                                string nuevoPuntoVenta = InputString("Ingrese el Nuevo Punto de Venta del Empleado: ");
                                double nuevaVenta1Mes = InputDouble("Ingrese las Nuevas Ventas del Primer Mes del Empleado: ");
                                double nuevaVenta2Mes = InputDouble("Ingrese las NuevAS Ventas del Segundo Mes del Empleado: ");
                                double nuevaVenta3Mes = InputDouble("Ingrese las Nuevo Ventas del Tercer Mes del Empleado: ");
                                DateTime fechaNac = InputFecha("Ingrese la Nueva Fecha de Nacimiento del Empleado (DD/MM/YYYY): ");
                                controller.ActualizarEmpleado(id, nuevaCedula, nuevoNombre, nuevoPuntoVenta, nuevaVenta1Mes, nuevaVenta2Mes, nuevaVenta3Mes);
                                controller.VerEmpleado(id);
                                break;
                            case "E":
                                id = InputId();
                                controller.EliminarEmpleado(id);
                                break;
                            case "S":
                                return;
                            case "H":
                                MostrarAyuda();
                                break;
                            case "C":
                                double totalVentas = empleadoService.PromedioTotalVentas();
                                Console.WriteLine("\n----------------------------------------------------------------------------------------------");
                                Console.WriteLine("\nEl Promedio de Ventas Totales es: (COP) {0:F2}", totalVentas);
                                break;
                            default:
                                Console.WriteLine("\nOpción no válida. Por favor, seleccione una opción válida.");
                                break;
                        }
                    }
                    catch (DAOException e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
            }
        }

        private static void MostrarAyuda()
        {
            Console.WriteLine("\n----------------------------------------------------------------------------------------------");
            Console.WriteLine("\nOpciones disponibles:\n");
            Console.WriteLine("[L]istar: Muestra la lista de empleados.");
            Console.WriteLine("[R]egistrar: Registra un nuevo empleado.");
            Console.WriteLine("[A]ctualizar: Actualiza un empleado existente.");
            Console.WriteLine("[E]liminar: Elimina un empleado.");
            Console.WriteLine("[C]alcular Promedio de Ventas Totales.");
            Console.WriteLine("[S]alir: Sale del programa.");
            Console.WriteLine("[H]elp: Muestra esta ayuda.");
        }

        private static Empleado InputEmpleado()
        {

            Console.WriteLine("\n----------------------------------------------------------------------------------------------");
            string cedula = InputStringNumero("\nIngrese el Número de Cédula del Empleado: ");
            string nombre = InputString("Ingrese el Nombre del Empleado: ");
            string puntoVenta = InputString("Ingrese el Punto de Venta del Empleado: ");
            double venta1Mes = InputDouble("Ingrese las ventas del Primer Mes del Empleado: ");
            double venta2Mes = InputDouble("Ingrese las ventas del Segundo Mes del Empleado: ");
            double venta3Mes = InputDouble("Ingrese las ventas del Tercer Mes del Empleado: ");
            DateTime fechaNac = InputFecha("Ingrese la fecha de Nacimiento del Empleado (DD/MM/YYYY): ");
            return new Empleado(cedula, nombre, puntoVenta, venta1Mes, venta2Mes, venta3Mes, fechaNac);
        }

        private static int InputId()
        {
            int id;
            while (true)
            {
                try
                {
                    Console.WriteLine("\n----------------------------------------------------------------------------------------------");
                    Console.Write("Ingrese un valor entero para el ID del empleado: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nError de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nError de formato de número");
                }
            }
            return id;
        }
        private static string InputStringNumero(string message)
        {
            string s;
            while (true)
            {
                Console.Write(message);
                s = Console.ReadLine()?.Trim();
                string patron = @"^\d+(\.\d+)?$";
                if (!string.IsNullOrEmpty(s) && s.Length >= 2 && Regex.IsMatch(s, patron))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nLa longitud de la cadena debe ser >= 2 y debe contener solo números positivos");
                }
            }
            return s;
        }

        private static string InputString(string message)
        {
            string s;
            while (true)
            {
                Console.Write(message);
                s = Console.ReadLine()?.Trim();
                string patron = "^[A-Za-z]+$";
                if (!string.IsNullOrEmpty(s) && s.Length >= 2 && Regex.IsMatch(s, patron))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nLa longitud de la cadena debe ser >= 2 y debe contener solo letras");
                }
            }
            return s;
        }

        private static double InputDouble(string message)
        {
            double value;
            while (true)
            {
                try
                {
                    Console.Write(message);
                    if (double.TryParse(Console.ReadLine(), out value))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nError de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nError de formato de número");
                }
            }
            return value;
        }
        private static DateTime InputFecha(string message)
        {
            DateTime fechaNacimiento;
            while (true)
            {
                try
                {
                    Console.Write(message);
                    if (DateTime.TryParse(Console.ReadLine(), out fechaNacimiento))
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("\nError de formato de fecha (DD/MM/YYYY)");
                    }
                }
                catch (FormatException)
                {
                    Console.Write("\nError de formato de fecha (DD/MM/YYYY)");
                }
            }
            return fechaNacimiento;
        }
    }
}
