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
    public partial class FormPeriodos : Form, IFormPeriodosView
    {
        private readonly FormPeriodosPresenter _presenter;

        public FormPeriodos()
        {
            InitializeComponent();
            ConfigurarControles();

            // 1. Instanciar repositorio y presenter
            var periodoRepo = new PeriodoRepository();
            _presenter = new FormPeriodosPresenter(this, periodoRepo);

            // 2. Vincular Botones con Eventos de la Interfaz
            btnNuevo.Click += (s, e) => NuevoClicked?.Invoke(this, EventArgs.Empty);
            btnEditar.Click += (s, e) => EditarClicked?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => CancelarClicked?.Invoke(this, EventArgs.Empty);
            btnEliminar.Click += (s, e) => EliminarClicked?.Invoke(this, EventArgs.Empty);

            // 3. Vincular Búsqueda y Filtros Dinámicos
            txtBuscador.TextChanged += (s, e) => BuscarTextChanged?.Invoke(this, EventArgs.Empty);
            chkEliminados.CheckedChanged += ChkEliminados_CheckedChanged;

            // 4. Vincular Evento de Selección de Tabla
            dgvPeriodos.CellClick += DgvPeriodos_CellClick;

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
            dgvPeriodos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPeriodos.ReadOnly = true;
            dgvPeriodos.AllowUserToAddRows = false;
        }

        // ==========================================
        // PROPIEDADES DE LA INTERFAZ
        // ==========================================

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PeriodoId { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Nombre { get => txtPeriodo.Text; set => txtPeriodo.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime FechaInicio { get => dtpFechaInicio.Value; set => dtpFechaInicio.Value = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime FechaFin { get => dtpFechaFin.Value; set => dtpFechaFin.Value = value; }

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

        public void CargarListaPeriodos(IEnumerable<Periodo> periodos)
        {
            dgvPeriodos.DataSource = periodos;

            // 1. Ocultar todas las columnas por defecto
            foreach (DataGridViewColumn col in dgvPeriodos.Columns)
            {
                col.Visible = false;
            }

            // 2. Mapeo de columnas visibles
            var columnasVisibles = new (string Campo, string Titulo, float Peso)[]
            {
                ("Nombre",      "Nombre del Periodo", 50f),
                ("FechaInicio", "F Inicio",    25f),
                ("FechaFin",    "F Término",   25f)
            };

            // 3. Aplicar configuración estandarizada
            for (int i = 0; i < columnasVisibles.Length; i++)
            {
                var (campo, titulo, peso) = columnasVisibles[i];

                if (dgvPeriodos.Columns[campo] is DataGridViewColumn col)
                {
                    col.Visible = true;
                    col.HeaderText = titulo;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    col.FillWeight = peso;
                    col.DisplayIndex = i;

                    // Dar formato corto a las columnas de fecha (dd/MM/yyyy)
                    if (campo == "FechaInicio" || campo == "FechaFin")
                    {
                        col.DefaultCellStyle.Format = "d";
                    }
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
            MessageBox.Show(mensaje, "Gestión de Periodos", MessageBoxButtons.OK, icono);
        }

        public void LimpiarCampos()
        {
            PeriodoId = 0;
            txtPeriodo.Clear();

            // Se resetean a la fecha actual al limpiar
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
        }

        public void HabilitarCampos(bool habilitar)
        {
            txtPeriodo.Enabled = habilitar;
            dtpFechaInicio.Enabled = habilitar;
            dtpFechaFin.Enabled = habilitar;
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

        private void DgvPeriodos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 1. Limpiar estados ANTES de cargar los nuevos datos
                _presenter.CancelarModos();

                // 2. Extraer la fila seleccionada
                DataGridViewRow fila = dgvPeriodos.Rows[e.RowIndex];

                // 3. Cargar la información a los controles
                PeriodoId = Convert.ToInt32(fila.Cells["Id"].Value ?? 0);
                Nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";

                if (DateTime.TryParse(fila.Cells["FechaInicio"].Value?.ToString(), out DateTime inicio))
                    FechaInicio = inicio;

                if (DateTime.TryParse(fila.Cells["FechaFin"].Value?.ToString(), out DateTime fin))
                    FechaFin = fin;

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