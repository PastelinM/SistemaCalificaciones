using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Repositories;
using SistemaCalificaciones.UI.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace SistemaCalificaciones.UI.Views
{
    public partial class FormMaterias : Form, IFormMateriasView
    {
        private readonly FormMateriasPresenter _presenter;

        public FormMaterias()
        {
            InitializeComponent();
            ConfigurarControles();

            // 1. Instanciar repositorio y presenter
            var materiaRepo = new MateriaRepository();
            _presenter = new FormMateriasPresenter(this, materiaRepo);

            // 2. Vincular Botones con Eventos de la Interfaz
            btnNuevo.Click += (s, e) => NuevoClicked?.Invoke(this, EventArgs.Empty);
            btnEditar.Click += (s, e) => EditarClicked?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => CancelarClicked?.Invoke(this, EventArgs.Empty);
            btnEliminar.Click += (s, e) => EliminarClicked?.Invoke(this, EventArgs.Empty);

            // 3. Vincular Búsqueda y Filtros Dinámicos
            txtBuscador.TextChanged += (s, e) => BuscarTextChanged?.Invoke(this, EventArgs.Empty);
            chkEliminados.CheckedChanged += ChkEliminados_CheckedChanged;

            // 4. Vincular Evento de Selección de Tabla
            dgvMaterias.CellClick += DgvMaterias_CellClick;

            // 5. Estado Inicial de Controles y Botones
            ConfigurarEstadoBotones(EstadoFormulario.Inicial);
        }

        private void ConfigurarControles()
        {
            // ComboBox de Filtro de Búsqueda
            cmbBusqueda.Items.Clear();
            cmbBusqueda.Items.AddRange(new string[] { "Nombre" });
            cmbBusqueda.SelectedIndex = 0;
            cmbBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;

            // Configuración del DataGridView
            dgvMaterias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaterias.ReadOnly = true;
            dgvMaterias.AllowUserToAddRows = false;
        }

        // ==========================================
        // PROPIEDADES DE LA INTERFAZ
        // ==========================================

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MateriaId { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Nombre { get => txtMateria.Text; set => txtMateria.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ColumnaBusqueda => cmbBusqueda.Text;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TextoBusqueda => txtBuscador.Text;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool MostrarEliminados => chkEliminados.Checked;

        // Eventos de la Interfaz
        public event EventHandler? NuevoClicked;
        public event EventHandler? EditarClicked;
        public event EventHandler? CancelarClicked;
        public event EventHandler? EliminarClicked;
        public event EventHandler? BuscarTextChanged;

        // ==========================================
        // MÉTODOS DE LA INTERFAZ Y CONTROL DE UX
        // ==========================================

        public void CargarListaMaterias(IEnumerable<Materia> materias)
        {
            dgvMaterias.DataSource = materias;

            // 1. Ocultar todas las columnas por defecto
            foreach (DataGridViewColumn col in dgvMaterias.Columns)
            {
                col.Visible = false;
            }

            // 2. Mapeo de columnas visibles
            var columnasVisibles = new (string Campo, string Titulo, float Peso)[]
            {
                ("Nombre", "Nombre de la Materia", 100f)
            };

            // 3. Aplicar configuración estandarizada
            for (int i = 0; i < columnasVisibles.Length; i++)
            {
                var (campo, titulo, peso) = columnasVisibles[i];

                if (dgvMaterias.Columns[campo] is DataGridViewColumn col)
                {
                    col.Visible = true;
                    col.HeaderText = titulo;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    col.FillWeight = peso;
                    col.DisplayIndex = i;
                }
            }
        }

        public void ActualizarTotalRegistros(int total)
        {
            lblTotal.Text = $"Total: {total}";
        }

        public void MostrarMensaje(string mensaje, bool esError)
        {
            MessageBoxIcon icono = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(mensaje, "Gestión de Materias", MessageBoxButtons.OK, icono);
        }

        public void LimpiarCampos()
        {
            MateriaId = 0;
            txtMateria.Clear();
        }

        public void HabilitarCampos(bool habilitar)
        {
            txtMateria.Enabled = habilitar;
        }

        public void ConfigurarEstadoBotones(EstadoFormulario estado)
        {
            bool viendoEliminados = chkEliminados.Checked;

            switch (estado)
            {
                case EstadoFormulario.Inicial:
                    HabilitarCampos(false);
                    btnNuevo.Enabled = !viendoEliminados;
                    btnNuevo.Text = "+ Nuevo";
                    btnEditar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnEliminar.Enabled = false;
                    break;

                case EstadoFormulario.Nuevo:
                    HabilitarCampos(true);
                    btnNuevo.Enabled = true;
                    btnNuevo.Text = "Guardar";
                    btnEditar.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnEliminar.Enabled = false;
                    break;

                case EstadoFormulario.FilaSeleccionada:
                    HabilitarCampos(false);
                    btnNuevo.Enabled = false;
                    btnNuevo.Text = "+ Nuevo";
                    btnEditar.Enabled = !viendoEliminados;
                    btnCancelar.Enabled = true;
                    btnEliminar.Enabled = true;
                    break;

                case EstadoFormulario.Editando:
                    HabilitarCampos(true);
                    btnNuevo.Enabled = false;
                    btnEditar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnEliminar.Enabled = false;
                    break;
            }
        }

        // ==========================================
        // MANEJO DE EVENTOS DE CONTROLES
        // ==========================================

        private void DgvMaterias_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 1. Limpiar estados ANTES de cargar los nuevos datos
                _presenter.CancelarModos();

                // 2. Extraer la fila seleccionada
                DataGridViewRow fila = dgvMaterias.Rows[e.RowIndex];

                // 3. Cargar la información a los controles
                MateriaId = Convert.ToInt32(fila.Cells["Id"].Value ?? 0);
                Nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";

                // 4. Cambiar estado visual de botones
                ConfigurarEstadoBotones(EstadoFormulario.FilaSeleccionada);
            }
        }

        private void ChkEliminados_CheckedChanged(object? sender, EventArgs e)
        {
            LimpiarCampos();

            if (chkEliminados.Checked)
            {
                btnEliminar.Text = "Restaurar";
                btnEliminar.BackColor = System.Drawing.Color.ForestGreen;
                btnEliminar.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                btnEliminar.Text = "Eliminar";
                btnEliminar.BackColor = System.Drawing.Color.IndianRed;
                btnEliminar.ForeColor = System.Drawing.Color.White;
            }

            ConfigurarEstadoBotones(EstadoFormulario.Inicial);
            BuscarTextChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}