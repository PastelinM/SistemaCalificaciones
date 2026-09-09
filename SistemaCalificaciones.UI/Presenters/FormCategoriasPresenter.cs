using System;
using System.Collections.Generic;
using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Repositories;

namespace SistemaCalificaciones.Presenters
{
    public class FormCategoriasPresenter
    {
        private readonly IFormCategoriasView _view;
        private readonly CategoriaRepository _repository;

        private bool _esNuevo;
        private bool _esEdicion;

        public FormCategoriasPresenter(IFormCategoriasView view, CategoriaRepository repository)
        {
            _view = view;
            _repository = repository;

            // Suscripción de eventos provenientes de la vista
            _view.NuevoClicked += OnNuevoClicked;
            _view.EditarClicked += OnEditarClicked;
            _view.CancelarClicked += OnCancelarClicked;
            _view.EliminarClicked += OnEliminarClicked;
            _view.BuscarTextChanged += OnBuscarTextChanged;

            // Inicializar vista
            InicializarVista();
        }

        private void InicializarVista()
        {
            CancelarModos();
            _view.LimpiarCampos();
            _view.ConfigurarEstadoBotones(EstadoFormulario.Inicial);
            CargarCategorias();
        }

        public void CargarCategorias()
        {
            try
            {
                var lista = _repository.Buscar(_view.ColumnaBusqueda, _view.TextoBusqueda, _view.MostrarEliminados);
                _view.CargarListaCategorias(lista);

                if (lista is ICollection<Categoria> coleccion)
                {
                    _view.ActualizarTotalRegistros(coleccion.Count);
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar categorías: {ex.Message}", esError: true);
            }
        }

        private void OnBuscarTextChanged(object? sender, EventArgs e)
        {
            CargarCategorias();
        }

        private void OnNuevoClicked(object? sender, EventArgs e)
        {
            // Estado 1: Iniciar modo de creación
            if (!_esNuevo)
            {
                _esNuevo = true;
                _esEdicion = false;
                _view.LimpiarCampos();
                _view.ConfigurarEstadoBotones(EstadoFormulario.Nuevo);
                return;
            }

            // Estado 2: Guardar el nuevo registro
            GuardarCategoria();
        }

        private void OnEditarClicked(object? sender, EventArgs e)
        {
            // Estado 1: Iniciar modo de edición desde una fila seleccionada
            if (!_esEdicion)
            {
                if (_view.CategoriaId <= 0)
                {
                    _view.MostrarMensaje("Seleccione una categoría para editar.", esError: true);
                    return;
                }

                _esEdicion = true;
                _esNuevo = false;
                _view.ConfigurarEstadoBotones(EstadoFormulario.Editando);
                return;
            }

            // Estado 2: Guardar los cambios del registro editado
            GuardarCategoria();
        }

        private void GuardarCategoria()
        {
            // Validaciones de UI
            if (string.IsNullOrWhiteSpace(_view.Nombre))
            {
                _view.MostrarMensaje("El nombre de la categoría es obligatorio.", esError: true);
                return;
            }

            if (!decimal.TryParse(_view.Porcentaje, out decimal porcentaje) || porcentaje < 0 || porcentaje > 100)
            {
                _view.MostrarMensaje("Ingrese un porcentaje válido (entre 0 y 100).", esError: true);
                return;
            }

            // Validación de duplicados
            if (_repository.ExisteNombre(_view.Nombre, _esEdicion ? _view.CategoriaId : 0))
            {
                _view.MostrarMensaje("Ya existe una categoría con el mismo nombre.", esError: true);
                return;
            }

            try
            {
                var categoria = new Categoria
                {
                    Id = _view.CategoriaId,
                    Nombre = _view.Nombre.Trim(),
                    Porcentaje = porcentaje,
                    Estatus = true
                };

                bool exito;
                if (_esNuevo)
                {
                    exito = _repository.Insertar(categoria);
                    if (exito) _view.MostrarMensaje("Categoría registrada correctamente.", esError: false);
                }
                else
                {
                    exito = _repository.Actualizar(categoria);
                    if (exito) _view.MostrarMensaje("Categoría actualizada correctamente.", esError: false);
                }

                if (exito)
                {
                    CancelarModos();
                    _view.LimpiarCampos();
                    _view.ConfigurarEstadoBotones(EstadoFormulario.Inicial);
                    CargarCategorias();
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Ocurrió un error al guardar: {ex.Message}", esError: true);
            }
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (_view.CategoriaId <= 0)
            {
                _view.MostrarMensaje("Seleccione una categoría de la lista.", esError: true);
                return;
            }

            bool esRestaurar = _view.MostrarEliminados;
            bool nuevoEstatus = esRestaurar; // Si está viendo eliminados, el nuevo estatus será TRUE (Restaurar)

            try
            {
                bool exito = _repository.CambiarEstatus(_view.CategoriaId, nuevoEstatus);
                if (exito)
                {
                    string accion = esRestaurar ? "restaurada" : "eliminada";
                    _view.MostrarMensaje($"La categoría fue {accion} correctamente.", esError: false);

                    CancelarModos();
                    _view.LimpiarCampos();
                    _view.ConfigurarEstadoBotones(EstadoFormulario.Inicial);
                    CargarCategorias();
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cambiar estatus de la categoría: {ex.Message}", esError: true);
            }
        }

        private void OnCancelarClicked(object? sender, EventArgs e)
        {
            CancelarModos();
            _view.LimpiarCampos();
            _view.ConfigurarEstadoBotones(EstadoFormulario.Inicial);
        }

        /// <summary>
        /// Reinicia las banderas internas de control de estado.
        /// </summary>
        public void CancelarModos()
        {
            _esNuevo = false;
            _esEdicion = false;
        }
    }
}