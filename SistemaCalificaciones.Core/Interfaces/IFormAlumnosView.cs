using System;

namespace SistemaCalificaciones.Core.interfaces
{
    public enum EstadoFormulario
    {
        Inicial,
        Nuevo,
        FilaSeleccionada,
        Editando
    }

    public interface IFormAlumnosView
    {
        // Campos de Captura
        int AlumnoId { get; set; }
        int InstitutoId { get; set; }
        string Matricula { get; set; }
        string Nombre { get; set; }
        string ApPaterno { get; set; }
        string ApMaterno { get; set; }
        string Genero { get; set; }
        string Grado { get; set; }
        string Grupo { get; set; }

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

        // Métodos de Respuesta a la UI
        void CargarInstitutos(object institutos);
        void CargarListaAlumnos(object alumnos);
        void ActualizarTotalRegistros(int total);
        void MostrarMensaje(string mensaje, bool esError);
        void LimpiarCampos();

        // Control de Estados y Botones (UX)
        void HabilitarCampos(bool habilitar);
        void ConfigurarEstadoBotones(EstadoFormulario estado);
    }
}