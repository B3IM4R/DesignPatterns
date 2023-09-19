using System;
using System.Collections.Generic;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class EmpleadoService
    {
        private IEmpleadoDao dao;

        public EmpleadoService(IEmpleadoDao dao)
        {
            this.dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        public bool RegistrarEmpleado(Empleado empleado)
        {
            try
            {
                return dao.RegistrarEmpleado(empleado);
            }
            catch (DAOException e)
            {
                Console.WriteLine("\nError al registrar el empleado: " + e.Message);
                return false;
            }
        }

        public bool ActualizarEmpleado(Empleado empleado)
        {
            try
            {
                return dao.ActualizarEmpleado(empleado);
            }
            catch (DAOException e)
            {
                Console.WriteLine("\nError al actualizar el empleado: " + e.Message);
                return false;
            }
        }

        public bool EliminarEmpleado(int id)
        {
            try
            {
                return dao.EliminarEmpleado(id);
            }
            catch (DAOException e)
            {
                Console.WriteLine("\nError al eliminar el empleado: " + e.Message);
                return false;
            }
        }

        public List<Empleado> ObtenerEmpleados()
        {
            try
            {
                return dao.ObtenerEmpleados();
            }
            catch (DAOException e)
            {
                Console.WriteLine("\nError al obtener los empleados: " + e.Message);
                return new List<Empleado>();
            }
        }

        public Empleado ObtenerEmpleadoPorId(int id)
        {
            try
            {
                return dao.ObtenerEmpleadoPorId(id);
            }
            catch (DAOException e)
            {
                Console.WriteLine("\nError al obtener el empleado por ID: " + e.Message);
                return null;
            }
        }

        public double PromedioTotalVentas()
        {
            List<Empleado> empleados = ObtenerEmpleados();
            double totalVentas = 0;

            foreach (Empleado empleado in empleados)
            {
                totalVentas += ((empleado.Venta1Mes + empleado.Venta2Mes + empleado.Venta3Mes) / 3);
            }

            return totalVentas;
        }
    }
}
