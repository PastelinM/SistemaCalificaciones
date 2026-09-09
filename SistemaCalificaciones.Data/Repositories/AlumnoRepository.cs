using System.Collections.Generic;
using System.Linq;
using Dapper;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Config;

namespace SistemaCalificaciones.Data.Repositories
{
    public class AlumnoRepository
    {
        // 1. INSERTAR
        public bool Insertar(Alumno alumno)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"INSERT INTO alumnos 
                               (instituto_id, matricula, nombre, ap_paterno, ap_materno, genero, grado, grupo, estatus) 
                               VALUES 
                               (@InstitutoId, @Matricula, @Nombre, @ApPaterno, @ApMaterno, @Genero, @Grado, @Grupo, true)";

                int filasAfectadas = conexion.Execute(sql, alumno);
                return filasAfectadas > 0;
            }
        }

        // 2. ACTUALIZAR (EDITAR)
        public bool Actualizar(Alumno alumno)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE alumnos 
                               SET instituto_id = @InstitutoId,
                                   matricula = @Matricula,
                                   nombre = @Nombre,
                                   ap_paterno = @ApPaterno,
                                   ap_materno = @ApMaterno,
                                   genero = @Genero,
                                   grado = @Grado,
                                   grupo = @Grupo
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, alumno);
                return filasAfectadas > 0;
            }
        }

        // 3. CAMBIAR ESTATUS (BORRADO LÓGICO O REACTIVACIÓN)
        public bool CambiarEstatus(int id, bool nuevoEstatus)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE alumnos 
                               SET estatus = @Estatus 
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, new { Id = id, Estatus = nuevoEstatus });
                return filasAfectadas > 0;
            }
        }

        // 4. BUSCAR Y FILTRAR (BÚSQUEDA DINÁMICA POR COLUMNA)
        public IEnumerable<Alumno> Buscar(string columna, string textoBusqueda, bool mostrarEliminados)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                // Mapear el valor del ComboBox a nombres reales de columnas SQL (evita inyección SQL)
                string columnaSQL = columna switch
                {
                    "Matricula" => "matricula",
                    "ApPaterno" => "ap_paterno",
                    _ => "nombre"
                };

                // Si el checkbox "Eliminados" está marcado, filtramos por estatus = false
                string condicionEstatus = mostrarEliminados ? "estatus = false" : "estatus = true";

                // ILIKE es propio de PostgreSQL para búsquedas insensibles a mayúsculas/minúsculas
                string sql = $@"SELECT id, 
                                       instituto_id AS InstitutoId, 
                                       matricula, 
                                       nombre, 
                                       ap_paterno AS ApPaterno, 
                                       ap_materno AS ApMaterno, 
                                       genero, 
                                       grado, 
                                       grupo, 
                                       creado_en AS CreadoEn, 
                                       estatus 
                                FROM alumnos 
                                WHERE {condicionEstatus} 
                                  AND {columnaSQL} ILIKE @Texto 
                                ORDER BY nombre ASC";

                return conexion.Query<Alumno>(sql, new { Texto = $"%{textoBusqueda}%" });
            }
        }

        public bool ExisteMatricula(string matricula, int idAlumnoExcluir = 0)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT COUNT(1) 
                       FROM Alumnos 
                       WHERE Matricula = @Matricula AND Id <> @IdExcluir";

                int cantidad = conexion.ExecuteScalar<int>(sql, new
                {
                    Matricula = matricula,
                    IdExcluir = idAlumnoExcluir
                });

                return cantidad > 0;
            }
        }
    }
}