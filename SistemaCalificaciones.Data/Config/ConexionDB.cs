using Npgsql;
using System;

namespace SistemaCalificaciones.Data.Config
{
    public static class ConexionDB
    {
        // Esta variable empieza vacía.
        private static string _cadenaConexion;

        /// <summary>
        /// Este método será llamado una sola vez por el proyecto UI cuando el programa arranque.
        /// Su único trabajo es recibir el string que venía del JSON y guardarlo aquí.
        /// </summary>
        public static void Inicializar(string cadenaConexion)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion))
                throw new ArgumentException("La cadena de conexion no puede estar vacia.");

            _cadenaConexion = cadenaConexion;
        }

        /// <summary>
        /// Este es el método que usarán tus Repositorios (como AlumnoRepository)
        /// cada vez que necesiten conectarse a PostgreSQL.
        /// </summary>
        public static NpgsqlConnection ObtenerConexion()
        {
            if (string.IsNullOrWhiteSpace(_cadenaConexion))
                throw new InvalidOperationException("La conexion no ha sido inicializada. Llama a ConexionDB.Inicializar() primero.");

            return new NpgsqlConnection(_cadenaConexion);
        }
    }
}