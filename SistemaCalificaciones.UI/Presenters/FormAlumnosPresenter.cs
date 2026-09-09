using System;
using System.Linq;
using System.Windows.Forms;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Repositories;
using SistemaCalificaciones.Core.interfaces;

namespace SistemaCalificaciones.UI.Presenters
{
    public class FormAlumnosPresenter
    {
        private readonly IFormAlumnosView _vista;
        private readonly AlumnoRepository _alumnoRepo;
        private readonly InstitutosRepository _institutoRepo;

        // Banderas de control de estado
        private bool _esNuevo = false;
        private bool _esEdicion = false;

        public FormAlumnosPresenter(IFormAlumnosView vista, AlumnoRepository alumnoRepo, InstitutosRepository institutoRepo)
        {
            _vista = vista;
            _alumnoRepo = alumnoRepo;
            _institutoRepo = institutoRepo;

            _vista.NuevoClicked += OnNuevoClicked;
            _vista.EditarClicked += OnEditarClicked;
            _vista.EliminarClicked += OnEliminarClicked;
            _vista.CancelarClicked += OnCancelarClicked;
            _vista.BuscarTextChanged += OnBuscarTextChanged;

            InicializarVista();
        }

        private void InicializarVista()
        {
            _vista.CargarInstitutos(_institutoRepo.ObtenerActivos());
            CargarLista();
            _vista.ConfigurarEstadoBotones(EstadoFormulario.Inicial);
        }

        private void CargarLista()
        {
            var lista = _alumnoRepo.Buscar(_vista.ColumnaBusqueda, _vista.TextoBusqueda, _vista.MostrarEliminados);
            _vista.CargarListaAlumnos(lista);
            _vista.ActualizarTotalRegistros(lista.Count());
        }

        private void OnBuscarTextChanged(object? sender, EventArgs e)
        {
            CargarLista();
        }

        private void OnNuevoClicked(object? sender, EventArgs e)
        {
            // PASO 1: Si no está en modo nuevo, prepara el formulario para escribir
            if (!_esNuevo)
            {
                _esNuevo = true;
                _esEdicion = false;
                _vista.LimpiarCampos();
                _vista.ConfigurarEstadoBotones(EstadoFormulario.Nuevo); // Habilita cajas de texto y cambia a "Guardar"
                return;
            }

            // PASO 2: Cuando vuelve a presionar (botón en modo "Guardar")
            if (!ValidarCampos()) return;

            if (_alumnoRepo.ExisteMatricula(_vista.Matricula.Trim()))
            {
                _vista.MostrarMensaje($"La matrícula '{_vista.Matricula}' ya está registrada.", true);
                return;
            }

            var alumno = CrearModeloDesdeVista();

            if (_alumnoRepo.Insertar(alumno))
            {
                _vista.MostrarMensaje("Alumno registrado exitosamente.", false);
                ResetearFormulario();
            }
        }

        private void OnEditarClicked(object? sender, EventArgs e)
        {
            if (_vista.AlumnoId <= 0)
            {
                _vista.MostrarMensaje("Seleccione un alumno de la lista para editar.", true);
                return;
            }

            // PASO 1: Si no está en modo edición, habilita los campos para modificar
            if (!_esEdicion)
            {
                _esEdicion = true;
                _esNuevo = false;
                _vista.ConfigurarEstadoBotones(EstadoFormulario.Editando); // Habilita cajas de texto
                return;
            }

            // PASO 2: Cuando vuelve a presionar "Editar", aplica la actualización
            if (!ValidarCampos()) return;

            if (_alumnoRepo.ExisteMatricula(_vista.Matricula.Trim(), _vista.AlumnoId))
            {
                _vista.MostrarMensaje($"La matrícula '{_vista.Matricula}' pertenece a otro alumno.", true);
                return;
            }

            var alumno = CrearModeloDesdeVista();
            alumno.Id = _vista.AlumnoId;

            if (_alumnoRepo.Actualizar(alumno))
            {
                _vista.MostrarMensaje("Alumno actualizado exitosamente.", false);
                ResetearFormulario();
            }
        }

        private void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (_vista.AlumnoId <= 0)
            {
                _vista.MostrarMensaje("Seleccione un alumno de la lista.", true);
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
                if (_alumnoRepo.CambiarEstatus(_vista.AlumnoId, nuevoEstatus))
                {
                    _vista.MostrarMensaje($"Alumno {accionTexto}do exitosamente.", false);
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
            if (string.IsNullOrWhiteSpace(_vista.Matricula) || string.IsNullOrWhiteSpace(_vista.Nombre))
            {
                _vista.MostrarMensaje("Los campos con asterisco (*) son obligatorios.", true);
                return false;
            }
            return true;
        }

        private Alumno CrearModeloDesdeVista()
        {
            return new Alumno
            {
                InstitutoId = _vista.InstitutoId,
                Matricula = _vista.Matricula.Trim(),
                Nombre = _vista.Nombre.Trim(),
                ApPaterno = _vista.ApPaterno.Trim(),
                ApMaterno = _vista.ApMaterno?.Trim(),
                Genero = _vista.Genero,
                Grado = int.TryParse(_vista.Grado, out int g) ? g : 1,
                Grupo = _vista.Grupo
            };
        }

        public void CancelarModos()
        {
            _esNuevo = false;
            _esEdicion = false;
        }
    }
}