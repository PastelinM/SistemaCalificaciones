using Dapper;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Config;
using System;
using System.Collections.Generic;

namespace SistemaCalificaciones.Data.Repositories
{
    public class CategoriaRepository
    {
        /// <summary>
        /// Consulta y filtra categorías activas o inactivas con soporte de búsqueda dinámica.
        /// </summary>
        public IEnumerable<Categoria> Buscar(string columna, string texto, bool mostrarEliminados)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT id AS Id, 
                                      nombre AS Nombre, 
                                      porcentaje AS Porcentaje, 
                                      estatus AS Estatus 
                               FROM Categorias 
                               WHERE estatus = @Estatus";

                // Aplicar filtro si el usuario escribió en la caja de búsqueda
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    if (columna == "Porcentaje")
                    {
                        sql += " AND CAST(porcentaje AS VARCHAR) LIKE @Texto";
                    }
                    else // Por defecto busca por Nombre
                    {
                        sql += " AND LOWER(nombre) LIKE LOWER(@Texto)";
                    }
                }

                sql += " ORDER BY id DESC";

                return conexion.Query<Categoria>(sql, new
                {
                    Estatus = !mostrarEliminados, // Si 'mostrarEliminados' es true, consulta estatus = false
                    Texto = $"%{texto.Trim()}%"
                });
            }
        }

        /// <summary>
        /// Obtiene únicamente las categorías activas (útil para llenar ComboBoxes en otros módulos).
        /// </summary>
        public IEnumerable<Categoria> ObtenerActivas()
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT id AS Id, nombre AS Nombre, porcentaje AS Porcentaje, estatus AS Estatus 
                               FROM Categorias 
                               WHERE estatus = TRUE 
                               ORDER BY nombre ASC";

                return conexion.Query<Categoria>(sql);
            }
        }

        /// <summary>
        /// Valida si un nombre de categoría ya existe en la base de datos.
        /// </summary>
        public bool ExisteNombre(string nombre, int idCategoriaExcluir = 0)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT COUNT(1) 
                               FROM Categorias 
                               WHERE LOWER(nombre) = LOWER(@Nombre) AND id <> @IdExcluir";

                int cantidad = conexion.ExecuteScalar<int>(sql, new
                {
                    Nombre = nombre.Trim(),
                    IdExcluir = idCategoriaExcluir
                });

                return cantidad > 0;
            }
        }

        /// <summary>
        /// Inserta una nueva categoría en la base de datos.
        /// </summary>
        public bool Insertar(Categoria categoria)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"INSERT INTO Categorias (nombre, porcentaje, estatus) 
                               VALUES (@Nombre, @Porcentaje, TRUE)";

                int filasAfectadas = conexion.Execute(sql, categoria);
                return filasAfectadas > 0;
            }
        }

        /// <summary>
        /// Actualiza el nombre y porcentaje de una categoría existente.
        /// </summary>
        public bool Actualizar(Categoria categoria)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE Categorias 
                               SET nombre = @Nombre, 
                                   porcentaje = @Porcentaje 
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, categoria);
                return filasAfectadas > 0;
            }
        }

        /// <summary>
        /// Aplica borrado lógico o restaura una categoría cambiando la bandera estatus.
        /// </summary>
        public bool CambiarEstatus(int id, bool nuevoEstatus)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE Categorias 
                               SET estatus = @Estatus 
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, new
                {
                    Id = id,
                    Estatus = nuevoEstatus
                });

                return filasAfectadas > 0;
            }
        }
    }
}