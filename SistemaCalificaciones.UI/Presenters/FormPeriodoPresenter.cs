using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Repositories;
using System;
using System.Linq;

namespace SistemaCalificaciones.UI.Presenters
{
    public class FormPeriodosPresenter
    {
        private readonly IFormPeriodosView _view;
        private readonly PeriodoRepository _repository;

        private bool _esNuevo;
        private bool _esEdicion;

        public FormPeriodosPresenter(IFormPeriodosView view, PeriodoRepository repository)
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
            CargarPeriodos();
        }

        public void CargarPeriodos()
        {
            try
            {
                var lista = _repository.Buscar(
                    _view.ColumnaBusqueda,
                    _view.TextoBusqueda,
                    _view.MostrarEliminados
                ).ToList();

                _view.CargarListaPeriodos(lista);
                _view.ActualizarTotalRegistros(lista.Count);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar los periodos: {ex.Message}", true);
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
                _view.MostrarMensaje("Ya existe un periodo con ese nombre.", true);
                return;
            }

            var periodo = new Periodo
            {
                Nombre = _view.Nombre.Trim(),
                FechaInicio = _view.FechaInicio,
                FechaFin = _view.FechaFin,
                Estatus = true
            };

            bool exito = _repository.Insertar(periodo);
            if (exito)
            {
                _view.MostrarMensaje("Periodo registrado con éxito.", false);
                CancelarModos();
                CargarPeriodos();
            }
            else
            {
                _view.MostrarMensaje("No se pudo registrar el periodo.", true);
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
            if (_view.PeriodoId <= 0)
            {
                _view.MostrarMensaje("Seleccione un periodo válido para editar.", true);
                return;
            }

            if (!ValidarCampos()) return;

            if (_repository.ExisteNombre(_view.Nombre, _view.PeriodoId))
            {
                _view.MostrarMensaje("Ya existe otro periodo con ese nombre.", true);
                return;
            }

            var periodo = new Periodo
            {
                Id = _view.PeriodoId,
                Nombre = _view.Nombre.Trim(),
                FechaInicio = _view.FechaInicio,
                FechaFin = _view.FechaFin
            };

            bool exito = _repository.Actualizar(periodo);
            if (exito)
            {
                _view.MostrarMensaje("Periodo actualizado con éxito.", false);
                CancelarModos();
                CargarPeriodos();
            }
            else
            {
                _view.MostrarMensaje("No se pudo actualizar el periodo.", true);
            }
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (_view.PeriodoId <= 0)
            {
                _view.MostrarMensaje("Seleccione un periodo de la lista.", true);
                return;
            }

            // Si se están mostrando eliminados, el nuevo estatus es TRUE (restaurar). Si no, es FALSE (eliminar).
            bool nuevoEstatus = _view.MostrarEliminados;
            bool exito = _repository.CambiarEstatus(_view.PeriodoId, nuevoEstatus);

            if (exito)
            {
                string accion = nuevoEstatus ? "restaurado" : "eliminado";
                _view.MostrarMensaje($"Periodo {accion} con éxito.", false);
                CancelarModos();
                CargarPeriodos();
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
            CargarPeriodos();
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
                _view.MostrarMensaje("El nombre del periodo es obligatorio.", true);
                return false;
            }

            if (!ValidarRangoFechas())
            {
                return false;
            }

            return true;
        }

        private bool ValidarRangoFechas()
        {
            // Comparamos únicamente la parte de la fecha (Date) omitiendo horas/minutos
            if (_view.FechaFin.Date < _view.FechaInicio.Date)
            {
                _view.MostrarMensaje("La fecha de término no puede ser menor a la fecha de inicio.", true);
                return false;
            }

            return true;
        }
    }
}