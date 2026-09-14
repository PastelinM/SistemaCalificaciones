using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SistemaCalificaciones.UI.Presenters
{
    public class FormActividadesPresenter
    {
        private readonly IFormActividadesView _vista;
        private readonly ActividadRepository _actividadRepo;

        // Repositorios de catálogos foráneos requeridos por los ComboBox
        private readonly CategoriaRepository _categoriaRepo;
        private readonly PeriodoRepository _periodoRepo;
        private readonly MateriaRepository _materiaRepo;
        private readonly CiclosRepository _cicloRepo;

        // Banderas de control de estado
        private bool _esNuevo = false;
        private bool _esEdicion = false;

        public FormActividadesPresenter(
            IFormActividadesView vista,
            ActividadRepository actividadRepo,
            CategoriaRepository categoriaRepo,
            PeriodoRepository periodoRepo,
            MateriaRepository materiaRepo,
            CiclosRepository cicloRepo)
        {
            _vista = vista;
            _actividadRepo = actividadRepo;
            _categoriaRepo = categoriaRepo;
            _periodoRepo = periodoRepo;
            _materiaRepo = materiaRepo;
            _cicloRepo = cicloRepo;

            _vista.NuevoClicked += OnNuevoClicked;
            _vista.EditarClicked += OnEditarClicked;
            _vista.EliminarClicked += OnEliminarClicked;
            _vista.CancelarClicked += OnCancelarClicked;
            _vista.BuscarTextChanged += OnBuscarTextChanged;

            InicializarVista();
        }

        private void InicializarVista()
        {
            // Cargar los 4 catálogos requeridos por el diseño de la UI
            _vista.CargarCategorias(_categoriaRepo.ObtenerActivos());
            _vista.CargarPeriodos(_periodoRepo.ObtenerActivos());
            _vista.CargarMaterias(_materiaRepo.ObtenerActivos());
            _vista.CargarCiclosEscolares(_cicloRepo.ObtenerActivos());

            CargarLista();
            _vista.ConfigurarEstadoBotones(EstadoFormulario.Inicial);
        }

        private void CargarLista()
        {
            var lista = _actividadRepo.Buscar(_vista.ColumnaBusqueda, _vista.TextoBusqueda, _vista.MostrarEliminados);
            _vista.CargarListaActividades(lista);
            _vista.ActualizarTotalRegistros(lista.Count());
        }

        private void OnBuscarTextChanged(object? sender, EventArgs e)
        {
            CargarLista();
        }

        private void OnNuevoClicked(object? sender, EventArgs e)
        {
            if (!_esNuevo)
            {
                _esNuevo = true;
                _esEdicion = false;
                _vista.LimpiarCampos();
                _vista.ConfigurarEstadoBotones(EstadoFormulario.Nuevo);
                return;
            }

            if (!ValidarCampos()) return;

            // Evitar duplicidad de títulos en la misma materia
            if (_actividadRepo.ExisteTitulo(_vista.Titulo.Trim(), _vista.MateriaId))
            {
                _vista.MostrarMensaje($"El título '{_vista.Titulo}' ya existe en esta materia.", true);
                return;
            }

            var actividad = CrearModeloDesdeVista();

            if (_actividadRepo.Insertar(actividad))
            {
                _vista.MostrarMensaje("Actividad registrada exitosamente.", false);
                ResetearFormulario();
            }
        }

        private void OnEditarClicked(object? sender, EventArgs e)
        {
            if (_vista.Id <= 0)
            {
                _vista.MostrarMensaje("Seleccione una actividad de la lista para editar.", true);
                return;
            }

            if (!_esEdicion)
            {
                _esEdicion = true;
                _esNuevo = false;
                _vista.ConfigurarEstadoBotones(EstadoFormulario.Editando);
                return;
            }

            if (!ValidarCampos()) return;

            if (_actividadRepo.ExisteTitulo(_vista.Titulo.Trim(), _vista.MateriaId, _vista.Id))
            {
                _vista.MostrarMensaje($"El título '{_vista.Titulo}' pertenece a otra actividad.", true);
                return;
            }

            var actividad = CrearModeloDesdeVista();
            actividad.Id = _vista.Id;

            if (_actividadRepo.Actualizar(actividad))
            {
                _vista.MostrarMensaje("Actividad actualizada exitosamente.", false);
                ResetearFormulario();
            }
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (_vista.Id <= 0)
            {
                _vista.MostrarMensaje("Seleccione una actividad de la lista.", true);
                return;
            }

            bool nuevoEstatus = _vista.MostrarEliminados;
            string accionTexto = nuevoEstatus ? "restaurar" : "eliminar";

            var confirmacion = MessageBox.Show(
                $"¿Está seguro de que desea {accionTexto} este registro?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion == DialogResult.Yes)
            {
                if (_actividadRepo.CambiarEstatus(_vista.Id, nuevoEstatus))
                {
                    _vista.MostrarMensaje($"Actividad {accionTexto}da exitosamente.", false);
                    ResetearFormulario();
                }
            }
        }

        private void OnCancelarClicked(object? sender, EventArgs e)
        {
            ResetearFormulario();
        }

        private void ResetearFormulario()
        {
            _esNuevo = false;
            _esEdicion = false;
            _vista.LimpiarCampos();
            CargarLista();
            _vista.ConfigurarEstadoBotones(EstadoFormulario.Inicial);
        }

        private bool ValidarCampos()
        {
            // Según la imagen, TODOS los campos tienen asterisco (*), incluyendo Descripción
            if (_vista.CategoriaId <= 0 ||
                _vista.PeriodoId <= 0 ||
                _vista.MateriaId <= 0 ||
                _vista.CicloEscolarId <= 0 ||
                string.IsNullOrWhiteSpace(_vista.Titulo) ||
                string.IsNullOrWhiteSpace(_vista.Descripcion) ||
                _vista.PuntajeMaximo <= 0 ||
                !_vista.FechaEntrega.HasValue)
            {
                _vista.MostrarMensaje("Los campos con asterisco (*) son obligatorios.", true);
                return false;
            }
            return true;
        }

        private Actividad CrearModeloDesdeVista()
        {
            return new Actividad
            {
                CategoriaId = _vista.CategoriaId,
                PeriodoId = _vista.PeriodoId,
                MateriaId = _vista.MateriaId,
                CicloEscolarId = _vista.CicloEscolarId,
                Titulo = _vista.Titulo.Trim(),
                Descripcion = _vista.Descripcion?.Trim(),
                PuntajeMaximo = _vista.PuntajeMaximo,
                FechaEntrega = _vista.FechaEntrega ?? DateTime.Now
            };
        }

        public void CancelarModos()
        {
            _esNuevo = false;
            _esEdicion = false;
        }
    }
}