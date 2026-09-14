using System;

namespace SistemaCalificaciones.Core.interfaces
{
    public interface IFormActividadesView
    {
        // Campos de Captura
        int Id { get; set; }
        int CategoriaId { get; set; }
        int PeriodoId { get; set; }
        int MateriaId { get; set; }
        int CicloEscolarId { get; set; }
        string Titulo { get; set; }
        string? Descripcion { get; set; }
        decimal PuntajeMaximo { get; set; }
        DateTime? FechaEntrega { get; set; }
        bool? Estatus { get; set; }

        // Campos de Búsqueda y Filtros
        string ColumnaBusqueda { get; }
        string TextoBusqueda { get; }
        bool MostrarEliminados { get; }

        // Eventos de los Botones
        event EventHandler NuevoClicked;
        event EventHandler EditarClicked;
        event EventHandler CancelarClicked;
        event EventHandler EliminarClicked;
        event EventHandler BuscarTextChanged;
        event EventHandler RestaurarClicked;

        // Métodos de Respuesta a la UI (Incluyendo catálogos foráneos)
        void CargarCategorias(object categorias);
        void CargarPeriodos(object periodos);
        void CargarMaterias(object materias);
        void CargarCiclosEscolares(object ciclos);
        void CargarListaActividades(object actividades);

        void ActualizarTotalRegistros(int total);
        void MostrarMensaje(string mensaje, bool esError);
        void LimpiarCampos();

        // Control de Estados y Botones (UX)
        void HabilitarCampos(bool habilitar);
        void ConfigurarEstadoBotones(EstadoFormulario estado);
    }
}