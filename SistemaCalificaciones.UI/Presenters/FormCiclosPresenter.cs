using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Repositories;
using System;
using System.Linq;

namespace SistemaCalificaciones.UI.Presenters
{
    public class FormCiclosPresenter
    {
        private readonly IFormCiclosView _view;
        private readonly CiclosRepository _repository;

        private bool _esNuevo;
        private bool _esEdicion;

        public FormCiclosPresenter(IFormCiclosView view, CiclosRepository repository)
        {
            _view = view;
            _repository = repository;

            // 1. Vincular Eventos de la Interfaz con los Métodos del Presenter
            _view.NuevoClicked += OnNuevoClicked;
            _view.EditarClicked += OnEditarClicked;
            _view.CancelarClicked += OnCancelarClicked;
            _view.EliminarClicked += OnEliminarClicked;
            _view.BuscarTextChanged += OnBuscarTextChanged;

            // 2. Carga Inicial de Datos
            CargarCiclos();
        }

        public void CargarCiclos()
        {
            try
            {
                // Uso de Buscar con la columna, texto de búsqueda y filtro de eliminados
                var lista = _repository.Buscar(
                    _view.ColumnaBusqueda,
                    _view.TextoBusqueda,
                    _view.MostrarEliminados
                ).ToList();

                _view.CargarListaCiclosEscolares(lista);
                _view.ActualizarTotalRegistros(lista.Count);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar los ciclos escolares: {ex.Message}", true);
            }
        }

        private void OnNuevoClicked(object? sender, EventArgs e)
        {
            if (!_esNuevo)
            {
                // Entrar a Modo 'Nuevo'
                _esNuevo = true;
                _esEdicion = false;
                _view.LimpiarCampos();
                _view.ConfigurarEstadoBotones(EstadoFormulario.Nuevo);
                return;
            }

            // Confirmar y Guardar Registro
            if (!ValidarCampos()) return;

            // Validación de duplicados
            if (_repository.ExisteNombre(_view.Nombre))
            {
                _view.MostrarMensaje("Ya existe un ciclo escolar con ese nombre.", true);
                return;
            }

            var ciclo = new CicloEscolar
            {
                Nombre = _view.Nombre.Trim(),
                Estatus = true
            };

            bool exito = _repository.Insertar(ciclo);
            if (exito)
            {
                _view.MostrarMensaje("Ciclo escolar registrado con éxito.", false);
                CancelarModos();
                CargarCiclos();
            }
            else
            {
                _view.MostrarMensaje("No se pudo registrar el ciclo escolar.", true);
            }
        }

        private void OnEditarClicked(object? sender, EventArgs e)
        {
            if (!_esEdicion)
            {
                // Entrar a Modo 'Edición'
                _esEdicion = true;
                _esNuevo = false;
                _view.ConfigurarEstadoBotones(EstadoFormulario.Editando);
                return;
            }

            // Confirmar y Guardar Cambios
            if (_view.CicloId <= 0)
            {
                _view.MostrarMensaje("Seleccione un ciclo escolar válido para editar.", true);
                return;
            }

            if (!ValidarCampos()) return;

            // Validación de duplicados excluyendo el ID actual
            if (_repository.ExisteNombre(_view.Nombre, _view.CicloId))
            {
                _view.MostrarMensaje("Ya existe otro ciclo escolar con ese nombre.", true);
                return;
            }

            var ciclo = new CicloEscolar
            {
                Id = _view.CicloId,
                Nombre = _view.Nombre.Trim()
            };

            bool exito = _repository.Actualizar(ciclo);
            if (exito)
            {
                _view.MostrarMensaje("Ciclo escolar actualizado con éxito.", false);
                CancelarModos();
                CargarCiclos();
            }
            else
            {
                _view.MostrarMensaje("No se pudo actualizar el ciclo escolar.", true);
            }
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (_view.CicloId <= 0)
            {
                _view.MostrarMensaje("Seleccione un ciclo escolar de la lista.", true);
                return;
            }

            // Si se están mostrando eliminados, el nuevo estatus es TRUE (restaurar). Si no, es FALSE (eliminar).
            bool nuevoEstatus = _view.MostrarEliminados;
            bool exito = _repository.CambiarEstatus(_view.CicloId, nuevoEstatus);

            if (exito)
            {
                string accion = nuevoEstatus ? "restaurado" : "eliminado";
                _view.MostrarMensaje($"Ciclo escolar {accion} con éxito.", false);
                CancelarModos();
                CargarCiclos();
            }
            else
            {
                _view.MostrarMensaje("No se pudo completar la operación.", true);
            }
        }

        private void OnCancelarClicked(object? sender, EventArgs e)
        {
            CancelarModos();
        }

        private void OnBuscarTextChanged(object? sender, EventArgs e)
        {
            CargarCiclos();
        }

        public void CancelarModos()
        {
            _esNuevo = false;
            _esEdicion = false;
            _view.LimpiarCampos();
            _view.ConfigurarEstadoBotones(EstadoFormulario.Inicial);
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(_view.Nombre))
            {
                _view.MostrarMensaje("El nombre del ciclo escolar es obligatorio.", true);
                return false;
            }

            return true;
        }
    }
}