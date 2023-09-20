using System.Collections.Generic;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public interface IEmpleadoDao
    {
        bool RegistrarEmpleado(Empleado empleado);
        List<Empleado> ObtenerEmpleados();
        bool ActualizarEmpleado(Empleado empleado);
        bool EliminarEmpleado(int id);
        Empleado ObtenerEmpleadoPorId(int id);
    }
}
