using System.Collections.Generic;
using System.Linq;
using Dapper;
using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Config;

namespace SistemaCalificaciones.Data.Repositories
{
    public class CiclosRepository 
    {
        // 1. INSERTAR
        public bool Insertar(CicloEscolar ciclo)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"INSERT INTO Ciclos_Escolares 
                               (nombre, estatus) 
                               VALUES 
                               (@Nombre, true)";

                int filasAfectadas = conexion.Execute(sql, ciclo);
                return filasAfectadas > 0;
            }
        }

        // 2. ACTUALIZAR (EDITAR)
        public bool Actualizar(CicloEscolar ciclo)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE Ciclos_Escolares 
                               SET nombre = @Nombre 
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, ciclo);
                return filasAfectadas > 0;
            }
        }

        // 3. CAMBIAR ESTATUS (BORRADO LÓGICO O REACTIVACIÓN)
        public bool CambiarEstatus(int id, bool nuevoEstatus)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE Ciclos_Escolares 
                               SET estatus = @Estatus 
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, new { Id = id, Estatus = nuevoEstatus });
                return filasAfectadas > 0;
            }
        }

        // 4. BUSCAR Y FILTRAR (BÚSQUEDA DINÁMICA POR COLUMNA)
        public IEnumerable<CicloEscolar> Buscar(string columna, string textoBusqueda, bool mostrarEliminados)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                // Mapear el valor del ComboBox a nombres reales de columnas SQL (evita inyección SQL)
                string columnaSQL = columna switch
                {
                    _ => "nombre"
                };

                // Si el checkbox "Eliminados" está marcado, filtramos por estatus = false
                string condicionEstatus = mostrarEliminados ? "estatus = false" : "estatus = true";

                // ILIKE es propio de PostgreSQL para búsquedas insensibles a mayúsculas/minúsculas
                string sql = $@"SELECT id AS Id, 
                                       nombre AS Nombre, 
                                       estatus AS Estatus 
                                FROM Ciclos_Escolares 
                                WHERE {condicionEstatus} 
                                  AND {columnaSQL} ILIKE @Texto 
                                ORDER BY id DESC";

                return conexion.Query<CicloEscolar>(sql, new { Texto = $"%{textoBusqueda}%" });
            }
        }

        // 5. VALIDAR NOMBRE EXISTENTE
        public bool ExisteNombre(string nombre, int idCicloExcluir = 0)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT COUNT(1) 
                               FROM Ciclos_Escolares 
                               WHERE LOWER(nombre) = LOWER(@Nombre) AND id <> @IdExcluir";

                int cantidad = conexion.ExecuteScalar<int>(sql, new
                {
                    Nombre = nombre.Trim(),
                    IdExcluir = idCicloExcluir
                });

                return cantidad > 0;
            }
        }

        // 6. OBTENER ACTIVOS (PARA COMBOBOXES EN OTROS MÓDULOS)
        public IEnumerable<CicloEscolar> ObtenerActivos()
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT id AS Id, 
                                       nombre AS Nombre, 
                                       estatus AS Estatus 
                                FROM Ciclos_Escolares 
                                WHERE estatus = true 
                                ORDER BY nombre ASC";

                return conexion.Query<CicloEscolar>(sql);
            }
        }
    }
}