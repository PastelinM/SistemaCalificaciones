using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Repositories;
using System;
using System.Linq;

namespace SistemaCalificaciones.UI.Presenters
{
    public class FormInstitutosPresenter
    {
        private readonly IFormInstitutosView _view;
        private readonly InstitutosRepository _repository;

        private bool _esNuevo;
        private bool _esEdicion;

        public FormInstitutosPresenter(IFormInstitutosView view, InstitutosRepository repository)
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
            CargarInstitutos();
        }

        public void CargarInstitutos()
        {
            try
            {
                var lista = _repository.Buscar(
                    _view.ColumnaBusqueda,
                    _view.TextoBusqueda,
                    _view.MostrarEliminados
                ).ToList();

                _view.CargarListaInstitutos(lista);
                _view.ActualizarTotalRegistros(lista.Count);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar los institutos: {ex.Message}", true);
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
                _view.MostrarMensaje("Ya existe un instituto con ese nombre.", true);
                return;
            }

            var instituto = new Instituto
            {
                Nombre = _view.Nombre.Trim(),
                Domicilio = _view.Domicilio?.Trim() ?? string.Empty,
                CodigoPostal = _view.CodigoPostal?.Trim() ?? string.Empty,
                Referencia = _view.Referencia?.Trim() ?? string.Empty,
                Estatus = true
            };

            bool exito = _repository.Insertar(instituto);
            if (exito)
            {
                _view.MostrarMensaje("Instituto registrado con éxito.", false);
                CancelarModos();
                CargarInstitutos();
            }
            else
            {
                _view.MostrarMensaje("No se pudo registrar el instituto.", true);
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
            if (_view.InstitutoId <= 0)
            {
                _view.MostrarMensaje("Seleccione un instituto válido para editar.", true);
                return;
            }

            if (!ValidarCampos()) return;

            if (_repository.ExisteNombre(_view.Nombre, _view.InstitutoId))
            {
                _view.MostrarMensaje("Ya existe otro instituto con ese nombre.", true);
                return;
            }

            var instituto = new Instituto
            {
                Id = _view.InstitutoId,
                Nombre = _view.Nombre.Trim(),
                Domicilio = _view.Domicilio?.Trim() ?? string.Empty,
                CodigoPostal = _view.CodigoPostal?.Trim() ?? string.Empty,
                Referencia = _view.Referencia?.Trim() ?? string.Empty
            };

            bool exito = _repository.Actualizar(instituto);
            if (exito)
            {
                _view.MostrarMensaje("Instituto actualizado con éxito.", false);
                CancelarModos();
                CargarInstitutos();
            }
            else
            {
                _view.MostrarMensaje("No se pudo actualizar el instituto.", true);
            }
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (_view.InstitutoId <= 0)
            {
                _view.MostrarMensaje("Seleccione un instituto de la lista.", true);
                return;
            }

            // Si se están mostrando eliminados, el nuevo estatus es TRUE (restaurar). Si no, es FALSE (eliminar).
            bool nuevoEstatus = _view.MostrarEliminados;
            bool exito = _repository.CambiarEstatus(_view.InstitutoId, nuevoEstatus);

            if (exito)
            {
                string accion = nuevoEstatus ? "restaurado" : "eliminado";
                _view.MostrarMensaje($"Instituto {accion} con éxito.", false);
                CancelarModos();
                CargarInstitutos();
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
            CargarInstitutos();
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
                _view.MostrarMensaje("El nombre del instituto es obligatorio.", true);
                return false;
            }

            return true;
        }
    }
}