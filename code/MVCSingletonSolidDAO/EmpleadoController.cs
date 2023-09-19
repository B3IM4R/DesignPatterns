using System;
using System.Collections.Generic;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class EmpleadoController
    {
        private EmpleadoService empleadoService;
        private EmpleadoView vista;

        public EmpleadoController(EmpleadoService empleadoService, EmpleadoView vista)
        {
            this.empleadoService = empleadoService ?? throw new ArgumentNullException(nameof(empleadoService));
            this.vista = vista ?? throw new ArgumentNullException(nameof(vista));
        }

        public void ListarEmpleados()
        {
            try
            {
                List<Empleado> empleados = empleadoService.ObtenerEmpleados();
                vista.MostrarEmpleados(empleados);
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("\nError al listar empleados: " + e.Message);
            }
        }

        public void VerEmpleado(int id)
        {
            try
            {
                Empleado empleado = empleadoService.ObtenerEmpleadoPorId(id);

                if (empleado != null)
                {
                    vista.MostrarEmpleado(empleado);
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró ningún empleado con el ID {id}.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("\nError al obtener el empleado: " + e.Message);
            }
        }

        public void RegistrarEmpleado(Empleado empleado)
        {
            try
            {
                if (empleadoService.RegistrarEmpleado(empleado))
                {
                    Console.WriteLine("\n----------------------------------------------------------------------------------------------");
                    Console.WriteLine("\n--> Registro exitoso: " + empleado.Id);
                    vista.MostrarEmpleado(empleado);
                }
                else
                {
                    Console.WriteLine("\nError al registrar el empleado.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("\nError al registrar el empleado: " + e.Message);
            }
        }

        public void ActualizarEmpleado(int id, string nuevaCedula, string nuevoNombre, string nuevoPuntoVenta, double nuevaVenta1Mes, double nuevaVenta2Mes, double nuevaVenta3Mes)
        {
            try
            {
                Empleado empleadoExistente = empleadoService.ObtenerEmpleadoPorId(id);

                if (empleadoExistente != null)
                {
                    Console.WriteLine("\nDATOS ORIGINALES");
                    Console.WriteLine(empleadoExistente);

                    empleadoExistente.Cedula = nuevaCedula;
                    empleadoExistente.Nombre = nuevoNombre;
                    empleadoExistente.PuntoVenta = nuevoPuntoVenta;
                    empleadoExistente.Venta1Mes = nuevaVenta1Mes;
                    empleadoExistente.Venta2Mes = nuevaVenta2Mes;
                    empleadoExistente.Venta3Mes = nuevaVenta3Mes;

                    if (empleadoService.ActualizarEmpleado(empleadoExistente))
                    {
                        Console.WriteLine("\n----------------------------------------------------------------------------------------------");
                        Console.WriteLine("\n--> Actualización exitosa");
                    }
                    else
                    {
                        Console.WriteLine("\nError al actualizar el empleado.");
                    }
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró ningún empleado con el ID {id}.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("\nError al actualizar el empleado: " + e.Message);
            }
        }

        public void EliminarEmpleado(int id)
        {
            try
            {
                Empleado empleadoAEliminar = empleadoService.ObtenerEmpleadoPorId(id);

                if (empleadoAEliminar != null)
                {
                    if (empleadoService.EliminarEmpleado(id))
                    {
                        Console.WriteLine("\n----------------------------------------------------------------------------------------------");
                        Console.WriteLine("Empleado eliminado: " + empleadoAEliminar.Id);
                    }
                    else
                    {
                        Console.WriteLine("\nError al eliminar el empleado.");
                    }
                }
                else
                {
                    Console.WriteLine($"\nNo se encontró ningún empleado con el ID {id}.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("\nError al eliminar el empleado: " + e.Message);
            }
        }
    }
}
