using System.Collections.Generic;
using System.Linq;
using Dapper;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Config;

namespace SistemaCalificaciones.Data.Repositories
{
    public class MateriaRepository
    {
        // 1. INSERTAR
        public bool Insertar(Materia materia)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"INSERT INTO materias 
                               (nombre, estatus) 
                               VALUES 
                               (@Nombre, true)";

                int filasAfectadas = conexion.Execute(sql, materia);
                return filasAfectadas > 0;
            }
        }

        // 2. ACTUALIZAR (EDITAR)
        public bool Actualizar(Materia materia)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE materias 
                               SET nombre = @Nombre
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, materia);
                return filasAfectadas > 0;
            }
        }

        // 3. CAMBIAR ESTATUS (BORRADO LÓGICO O REACTIVACIÓN)
        public bool CambiarEstatus(int id, bool nuevoEstatus)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE materias 
                               SET estatus = @Estatus 
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, new { Id = id, Estatus = nuevoEstatus });
                return filasAfectadas > 0;
            }
        }

        // 4. BUSCAR Y FILTRAR (BÚSQUEDA DINÁMICA POR COLUMNA)
        public IEnumerable<Materia> Buscar(string columna, string textoBusqueda, bool mostrarEliminados)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string columnaSQL = columna switch
                {
                    "Id" => "id::text",
                    _ => "nombre"
                };

                string condicionEstatus = mostrarEliminados ? "estatus = false" : "estatus = true";

                string sql = $@"SELECT id AS Id, 
                                       nombre AS Nombre, 
                                       estatus AS Estatus 
                                FROM materias 
                                WHERE {condicionEstatus} 
                                  AND {columnaSQL} ILIKE @Texto 
                                ORDER BY nombre ASC";

                return conexion.Query<Materia>(sql, new { Texto = $"%{textoBusqueda}%" });
            }
        }

        // 5. VALIDAR NOMBRE EXISTENTE (EVITAR DUPLICADOS)
        public bool ExisteNombre(string nombre, int idMateriaExcluir = 0)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT COUNT(1) 
                               FROM materias 
                               WHERE LOWER(nombre) = LOWER(@Nombre) AND id <> @IdExcluir";

                int cantidad = conexion.ExecuteScalar<int>(sql, new
                {
                    Nombre = nombre.Trim(),
                    IdExcluir = idMateriaExcluir
                });

                return cantidad > 0;
            }
        }

        // 6. OBTENER ACTIVOS (PARA SELECTS / COMBOBOXES)
        public IEnumerable<Materia> ObtenerActivos()
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT id AS Id, 
                                      nombre AS Nombre, 
                                      estatus AS Estatus 
                               FROM materias 
                               WHERE estatus = true 
                               ORDER BY nombre ASC";

                return conexion.Query<Materia>(sql);
            }
        }
    }
}