using System.Collections.Generic;
using System.Linq;
using Dapper;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Config;
using SistemaCalificaciones.Core.interfaces; // Descomenta si creas la interfaz IInstitutosRepository

namespace SistemaCalificaciones.Data.Repositories
{
    public class InstitutosRepository // Agrega : IInstitutosRepository si utilizas la interfaz
    {
        // 1. INSERTAR
        public bool Insertar(Instituto instituto)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"INSERT INTO institutos 
                               (nombre, domicilio, codigo_postal, referencia, estatus) 
                               VALUES 
                               (@Nombre, @Domicilio, @CodigoPostal, @Referencia, true)";

                int filasAfectadas = conexion.Execute(sql, instituto);
                return filasAfectadas > 0;
            }
        }

        // 2. ACTUALIZAR (EDITAR)
        public bool Actualizar(Instituto instituto)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE institutos 
                               SET nombre = @Nombre,
                                   domicilio = @Domicilio,
                                   codigo_postal = @CodigoPostal,
                                   referencia = @Referencia
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, instituto);
                return filasAfectadas > 0;
            }
        }

        // 3. CAMBIAR ESTATUS (BORRADO LÓGICO O REACTIVACIÓN)
        public bool CambiarEstatus(int id, bool nuevoEstatus)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE institutos 
                               SET estatus = @Estatus 
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, new { Id = id, Estatus = nuevoEstatus });
                return filasAfectadas > 0;
            }
        }

        // 4. BUSCAR Y FILTRAR (BÚSQUEDA DINÁMICA POR COLUMNA)
        public IEnumerable<Instituto> Buscar(string columna, string textoBusqueda, bool mostrarEliminados)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                // Mapeo para prevenir inyección SQL
                string columnaSQL = columna switch
                {
                    "CodigoPostal" => "codigo_postal",
                    "Domicilio" => "domicilio",
                    _ => "nombre"
                };

                string condicionEstatus = mostrarEliminados ? "estatus = false" : "estatus = true";

                string sql = $@"SELECT id AS Id, 
                                       nombre AS Nombre, 
                                       domicilio AS Domicilio,
                                       codigo_postal AS CodigoPostal,
                                       referencia AS Referencia,
                                       creado_en AS CreadoEn,
                                       estatus AS Estatus 
                                FROM institutos 
                                WHERE {condicionEstatus} 
                                  AND {columnaSQL} ILIKE @Texto 
                                ORDER BY nombre ASC";

                return conexion.Query<Instituto>(sql, new { Texto = $"%{textoBusqueda}%" });
            }
        }

        // 5. VALIDAR NOMBRE EXISTENTE (EVITAR DUPLICADOS)
        public bool ExisteNombre(string nombre, int idInstitutoExcluir = 0)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT COUNT(1) 
                               FROM institutos 
                               WHERE LOWER(nombre) = LOWER(@Nombre) AND id <> @IdExcluir";

                int cantidad = conexion.ExecuteScalar<int>(sql, new
                {
                    Nombre = nombre.Trim(),
                    IdExcluir = idInstitutoExcluir
                });

                return cantidad > 0;
            }
        }

        // 6. OBTENER ACTIVOS (PARA COMBOBOXES)
        public IEnumerable<Instituto> ObtenerActivos()
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"SELECT id AS Id, 
                                      nombre AS Nombre, 
                                      estatus AS Estatus 
                               FROM institutos 
                               WHERE estatus = true 
                               ORDER BY nombre ASC";

                return conexion.Query<Instituto>(sql);
            }
        }
    }
}