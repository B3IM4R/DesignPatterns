using System;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class DAOException : Exception
    {
        public DAOException() : base("\nError en la capa de acceso a datos.")
        {
        }

        public DAOException(string message) : base(message)
        {
        }

        public DAOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}