using System;
using System.Collections.Generic;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class EmpleadoView
    {
        public void MostrarEmpleado(Empleado empleado)
        {
            Console.WriteLine("\nDatos del Empleado\n\n" + empleado.ToString());
        }

        public void MostrarEmpleados(List<Empleado> empleados)
        {
            if (empleados.Count == 0)
            {
                Console.WriteLine("\nNo hay empleados para mostrar.");
                return;
            }

            Console.WriteLine("\nLista de Empleados");
            foreach (Empleado empleado in empleados)
            {
                Console.WriteLine("\n----------------------------------------------------------------------------------------------");
                Console.WriteLine(empleado.ToString());
            }
        }
    }
}
