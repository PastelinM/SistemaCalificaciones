using System.Collections.Generic;
using System.Linq;
using Dapper;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Config;

namespace SistemaCalificaciones.Data.Repositories
{
    public class PeriodoRepository
    {
        // 1. INSERTAR
        public bool Insertar(Periodo periodo)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"INSERT INTO periodos 
                               (nombre, fecha_inicio, fecha_fin, estatus) 
                               VALUES 
                               (@Nombre, @FechaInicio, @FechaFin, true)";

                int filasAfectadas = conexion.Execute(sql, periodo);
                return filasAfectadas > 0;
            }
        }

        // 2. ACTUALIZAR (EDITAR)
        public bool Actualizar(Periodo periodo)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE periodos 
                               SET nombre = @Nombre,
                                   fecha_inicio = @FechaInicio,
                                   fecha_fin = @FechaFin
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, periodo);
                return filasAfectadas > 0;
            }
        }

        // 3. CAMBIAR ESTATUS (BORRADO LÓGICO O REACTIVACIÓN)
        public bool CambiarEstatus(int id, bool nuevoEstatus)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE periodos 
                               SET estatus = @Estatus 
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, new { Id = id, Estatus = nuevoEstatus });
                return filasAfectadas > 0;
            }
        }

        // 4. BUSCAR Y FILTRAR
        public IEnumerable<Periodo> Buscar(string columna, string textoBusqueda, bool mostrarEliminados)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string columnaSQL = columna switch
                {
                    "Id" => "id::text",
                    _ => "nombre"
                };

                string condicionEstatus = mostrarEliminados ? "estatus = false" : "estatus = true";

                // Se castea fecha_inicio y fecha_fin a timestamp para compatibilidad directa con DateTime
                string sql = $@"SELECT id AS Id, 
                                       nombre AS Nombre, 
                                       fecha_inicio::timestamp AS FechaInicio,
                                       fecha_fin::timestamp AS FechaFin,
                                       estatus AS Estatus 
                                FROM periodos 
                                WHERE {condicionEstatus} 
                                  AND {columnaSQL} ILIKE @Texto 
                                ORDER BY nombre ASC";

                return conexion.Query<Periodo>(sql, new { Texto = $"%{textoBusqueda}%" });
            }
        }

        // 5. VALIDAR NOMBRE EXISTENTE
        public bool ExisteNombre(string nombre, int idPeriodoExcluir = 0)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT COUNT(1) 
                               FROM periodos 
                               WHERE LOWER(nombre) = LOWER(@Nombre) AND id <> @IdExcluir";

                int cantidad = conexion.ExecuteScalar<int>(sql, new
                {
                    Nombre = nombre.Trim(),
                    IdExcluir = idPeriodoExcluir
                });

                return cantidad > 0;
            }
        }

        // 6. OBTENER ACTIVOS
        public IEnumerable<Periodo> ObtenerActivos()
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT id AS Id, 
                                      nombre AS Nombre, 
                                      fecha_inicio::timestamp AS FechaInicio,
                                      fecha_fin::timestamp AS FechaFin,
                                      estatus AS Estatus 
                               FROM periodos 
                               WHERE estatus = true 
                               ORDER BY nombre ASC";

                return conexion.Query<Periodo>(sql);
            }
        }
    }
}