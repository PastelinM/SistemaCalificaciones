using System;

namespace SistemaCalificaciones.Core.interfaces
{
    public interface IFormCategoriasView
    {
        // Campos de Captura
        int CategoriaId { get; set; } // Identificador oculto para Edición / Baja
        string Nombre { get; set; }
        string Porcentaje { get; set; }

        // Campos de Búsqueda y Filtros
        string ColumnaBusqueda { get; } // Seleccionado en el ComboBox de búsqueda
        string TextoBusqueda { get; }   // Escrito en el TextBox de búsqueda
        bool MostrarEliminados { get; } // Estado del CheckBox para inactivos

        // Eventos de los Botones
        event EventHandler NuevoClicked;
        event EventHandler EditarClicked;
        event EventHandler CancelarClicked;
        event EventHandler EliminarClicked;
        event EventHandler BuscarTextChanged;

        // Métodos de Respuesta a la UI
        void CargarListaCategorias(object categorias);
        void ActualizarTotalRegistros(int total);
        void MostrarMensaje(string mensaje, bool esError);
        void LimpiarCampos();

        // Control de Estados y Usabilidad (UX)
        void HabilitarCampos(bool habilitar);
        void ConfigurarEstadoBotones(EstadoFormulario estado);
    }
}