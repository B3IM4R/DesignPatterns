using System;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PuntoVenta { get; set; }
        public double Venta1Mes { get; set; }
        public double Venta2Mes { get; set; }
        public double Venta3Mes { get; set; }
        public DateTime FechaNac { get; set; }

        public Empleado(int id, string cedula, string nombre, string puntoVenta, double venta1Mes, double venta2Mes, double venta3Mes, DateTime fechaNac)
        {
            if (venta1Mes < 0 || venta2Mes < 0 || venta3Mes < 0)
            {
                throw new ArgumentException("\nLas ventas ingresadas NO pueden ser negativas");
            }

            Id = id;
            Cedula = cedula ?? throw new ArgumentNullException(nameof(cedula));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            PuntoVenta = puntoVenta ?? throw new ArgumentNullException(nameof(puntoVenta));
            Venta1Mes = venta1Mes;
            Venta2Mes = venta2Mes;
            Venta3Mes = venta3Mes;
            FechaNac = fechaNac;
        }

        public Empleado(string cedula, string nombre, string puntoVenta, double venta1Mes, double venta2Mes, double venta3Mes, DateTime fechaNac)
        {
            if (venta1Mes < 0 || venta2Mes < 0 || venta3Mes < 0)
            {
                throw new ArgumentException("\nLas ventas ingresadas NO pueden ser negativas");
            }

            Cedula = cedula ?? throw new ArgumentNullException(nameof(cedula));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            PuntoVenta = puntoVenta ?? throw new ArgumentNullException(nameof(puntoVenta));
            Venta1Mes = venta1Mes;
            Venta2Mes = venta2Mes;
            Venta3Mes = venta3Mes;
            FechaNac = fechaNac;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nCédula: {Cedula}\nNombre: {Nombre}\nPunto de Venta: {PuntoVenta}\nVenta 1 Mes: {Venta1Mes}\nVenta 2 Mes: {Venta2Mes}\nVenta 3 Mes: {Venta3Mes}\nFecha de Nacimiento: {FechaNac}";
        }
    }
}
