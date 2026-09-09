using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Repositories;
using System;
using System.Linq;

namespace SistemaCalificaciones.UI.Presenters
{
    public class FormMateriasPresenter
    {
        private readonly IFormMateriasView _view;
        private readonly MateriaRepository _repository;

        private bool _esNuevo;
        private bool _esEdicion;

        public FormMateriasPresenter(IFormMateriasView view, MateriaRepository repository)
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
            CargarMaterias();
        }

        public void CargarMaterias()
        {
            try
            {
                var lista = _repository.Buscar(
                    _view.ColumnaBusqueda,
                    _view.TextoBusqueda,
                    _view.MostrarEliminados
                ).ToList();

                _view.CargarListaMaterias(lista);
                _view.ActualizarTotalRegistros(lista.Count);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar las materias: {ex.Message}", true);
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

            if (_repository.ExisteNombre(_view.Nombre))
            {
                _view.MostrarMensaje("Ya existe una materia con ese nombre.", true);
                return;
            }

            var materia = new Materia
            {
                Nombre = _view.Nombre.Trim(),
                Estatus = true
            };

            bool exito = _repository.Insertar(materia);
            if (exito)
            {
                _view.MostrarMensaje("Materia registrada con éxito.", false);
                CancelarModos();
                CargarMaterias();
            }
            else
            {
                _view.MostrarMensaje("No se pudo registrar la materia.", true);
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
            if (_view.MateriaId <= 0)
            {
                _view.MostrarMensaje("Seleccione una materia válida para editar.", true);
                return;
            }

            if (!ValidarCampos()) return;

            if (_repository.ExisteNombre(_view.Nombre, _view.MateriaId))
            {
                _view.MostrarMensaje("Ya existe otra materia con ese nombre.", true);
                return;
            }

            var materia = new Materia
            {
                Id = _view.MateriaId,
                Nombre = _view.Nombre.Trim()
            };

            bool exito = _repository.Actualizar(materia);
            if (exito)
            {
                _view.MostrarMensaje("Materia actualizada con éxito.", false);
                CancelarModos();
                CargarMaterias();
            }
            else
            {
                _view.MostrarMensaje("No se pudo actualizar la materia.", true);
            }
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (_view.MateriaId <= 0)
            {
                _view.MostrarMensaje("Seleccione una materia de la lista.", true);
                return;
            }

            // Si se están mostrando eliminados, el nuevo estatus es TRUE (restaurar). Si no, es FALSE (eliminar).
            bool nuevoEstatus = _view.MostrarEliminados;
            bool exito = _repository.CambiarEstatus(_view.MateriaId, nuevoEstatus);

            if (exito)
            {
                string accion = nuevoEstatus ? "restaurada" : "eliminada";
                _view.MostrarMensaje($"Materia {accion} con éxito.", false);
                CancelarModos();
                CargarMaterias();
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
            CargarMaterias();
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
                _view.MostrarMensaje("El nombre de la materia es obligatorio.", true);
                return false;
            }

            return true;
        }
    }
}