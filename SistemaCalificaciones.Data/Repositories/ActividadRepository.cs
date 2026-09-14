using Dapper;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Config;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCalificaciones.Data.Repositories
{
    public class ActividadRepository
    {
        // 1. INSERTAR
        public bool Insertar(Actividad actividad)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"INSERT INTO actividades 
                               (categoria_id, periodo_id, materia_id, ciclo_escolar_id, titulo, descripcion, puntaje_maximo, fecha_entrega, estatus) 
                               VALUES 
                               (@CategoriaId, @PeriodoId, @MateriaId, @CicloEscolarId, @Titulo, @Descripcion, @PuntajeMaximo, @FechaEntrega, true)";

                int filasAfectadas = conexion.Execute(sql, actividad);
                return filasAfectadas > 0;
            }
        }

        // 2. ACTUALIZAR (EDITAR)
        public bool Actualizar(Actividad actividad)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE actividades 
                               SET categoria_id = @CategoriaId,
                                   periodo_id = @PeriodoId,
                                   materia_id = @MateriaId,
                                   ciclo_escolar_id = @CicloEscolarId,
                                   titulo = @Titulo,
                                   descripcion = @Descripcion,
                                   puntaje_maximo = @PuntajeMaximo,
                                   fecha_entrega = @FechaEntrega
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, actividad);
                return filasAfectadas > 0;
            }
        }

        // 3. CAMBIAR ESTATUS (BORRADO LÓGICO O REACTIVACIÓN)
        public bool CambiarEstatus(int id, bool nuevoEstatus)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                string sql = @"UPDATE actividades 
                               SET estatus = @Estatus 
                               WHERE id = @Id";

                int filasAfectadas = conexion.Execute(sql, new { Id = id, Estatus = nuevoEstatus });
                return filasAfectadas > 0;
            }
        }

        // 4. BUSCAR Y FILTRAR (BÚSQUEDA DINÁMICA POR COLUMNA)
        public IEnumerable<Actividad> Buscar(string columna, string textoBusqueda, bool mostrarEliminados)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                // Mapear el valor del ComboBox a nombres reales de columnas SQL
                string columnaSQL = columna switch
                {
                    "Descripcion" => "descripcion",
                    _ => "titulo"
                };

                // Filtrar por estatus dependiendo del checkbox
                string condicionEstatus = mostrarEliminados ? "estatus = false" : "estatus = true";

                string sql = $@"SELECT id, 
                                       categoria_id AS CategoriaId, 
                                       periodo_id AS PeriodoId, 
                                       materia_id AS MateriaId, 
                                       ciclo_escolar_id AS CicloEscolarId, 
                                       titulo, 
                                       descripcion, 
                                       puntaje_maximo AS PuntajeMaximo, 
                                       fecha_entrega::timestamp AS FechaEntrega, 
                                       estatus 
                                FROM actividades 
                                WHERE {condicionEstatus} 
                                  AND {columnaSQL} ILIKE @Texto 
                                ORDER BY titulo ASC";

                return conexion.Query<Actividad>(sql, new { Texto = $"%{textoBusqueda}%" });
            }
        }

        // 5. VALIDACIÓN DE DUPLICADOS
        public bool ExisteTitulo(string titulo, int idMateria, int idActividadExcluir = 0)
        {
            using (var conexion = ConexionDB.ObtenerConexion())
            {
                // Verifica si ya existe una actividad con el mismo título en la misma materia
                string sql = @"SELECT COUNT(1) 
                               FROM actividades 
                               WHERE titulo = @Titulo 
                                 AND materia_id = @MateriaId 
                                 AND id <> @IdExcluir";

                int cantidad = conexion.ExecuteScalar<int>(sql, new
                {
                    Titulo = titulo,
                    MateriaId = idMateria,
                    IdExcluir = idActividadExcluir
                });

                return cantidad > 0;
            }
        }
    }
}