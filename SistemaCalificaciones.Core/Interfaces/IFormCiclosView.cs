using System;

namespace SistemaCalificaciones.Core.interfaces
{
    public interface IFormCiclosView
    {
        // ==========================================
        // PROPIEDADES DE DATOS / CAMPOS
        // ==========================================
        int CicloId { get; set; }
        string Nombre { get; set; }

        // ==========================================
        // PROPIEDADES DE BÚSQUEDA Y FILTROS
        // ==========================================
        string ColumnaBusqueda { get; }
        string TextoBusqueda { get; }
        bool MostrarEliminados { get; }

        // ==========================================
        // EVENTOS DE LA INTERFAZ
        // ==========================================
        event EventHandler? NuevoClicked;
        event EventHandler? EditarClicked;
        event EventHandler? CancelarClicked;
        event EventHandler? EliminarClicked;
        event EventHandler? BuscarTextChanged;
        event EventHandler? RestaurarClicked;

        // ==========================================
        // MÉTODOS DE CONTROL DE UX Y VISUALIZACIÓN
        // ==========================================
        void CargarListaCiclosEscolares(object ciclos);
        void ActualizarTotalRegistros(int total);
        void MostrarMensaje(string mensaje, bool esError);
        void LimpiarCampos();
        void HabilitarCampos(bool habilitar);
        void ConfigurarEstadoBotones(EstadoFormulario estado);
    }
}