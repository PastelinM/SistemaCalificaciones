using SistemaCalificaciones.Core.Models;
using System;
using System.Collections.Generic;

namespace SistemaCalificaciones.Core.interfaces
{
    public interface IFormInstitutosView
    {
        // Propiedades del Modelo Instituto
        int InstitutoId { get; set; }
        string Nombre { get; set; }
        string Domicilio { get; set; }
        string CodigoPostal { get; set; }
        string Referencia { get; set; }

        // Propiedades de Búsqueda y Filtros
        string ColumnaBusqueda { get; }
        string TextoBusqueda { get; }
        bool MostrarEliminados { get; }

        // Eventos de la Interfaz
        event EventHandler NuevoClicked;
        event EventHandler EditarClicked;
        event EventHandler EliminarClicked;
        event EventHandler CancelarClicked;
        event EventHandler BuscarTextChanged;

        // Métodos de Interacción con la UI
        void CargarListaInstitutos(IEnumerable<Instituto> institutos);
        void LimpiarCampos();
        void ConfigurarEstadoBotones(EstadoFormulario estado);
        void MostrarMensaje(string mensaje, bool esError);
        void ActualizarTotalRegistros(int total);
    }
}
