using SistemaCalificaciones.Core.Models;
using System;
using System.Collections.Generic;

namespace SistemaCalificaciones.Core.interfaces
{
    public interface IFormMateriasView
    {
        // Propiedades del Modelo Materia
        int MateriaId { get; set; }
        string Nombre { get; set; }

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
        void CargarListaMaterias(IEnumerable<Materia> materias);
        void LimpiarCampos();
        void ConfigurarEstadoBotones(EstadoFormulario estado);
        void MostrarMensaje(string mensaje, bool esError);
        void ActualizarTotalRegistros(int total);
    }
}