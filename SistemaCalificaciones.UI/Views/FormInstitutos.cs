using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Core.Models;
using SistemaCalificaciones.Data.Repositories;
using SistemaCalificaciones.UI.Presenters;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SistemaCalificaciones.UI.Views
{
    public partial class FormInstitutos : Form, IFormInstitutosView
    {
        private readonly FormInstitutosPresenter _presenter;

        public FormInstitutos()
        {
            InitializeComponent();
            ConfigurarControles();

            // 1. Instanciar capas MVP
            var institutoRepo = new InstitutosRepository();
            _presenter = new FormInstitutosPresenter(this, institutoRepo);

            // 2. Vincular Botones con Eventos de la Interfaz
            btnNuevo.Click += (s, e) => NuevoClicked?.Invoke(this, EventArgs.Empty);
            btnEditar.Click += (s, e) => EditarClicked?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => CancelarClicked?.Invoke(this, EventArgs.Empty);
            btnEliminar.Click += (s, e) => EliminarClicked?.Invoke(this, EventArgs.Empty);

            // 3. Vincular Búsqueda y Filtros Dinámicos
            txtBuscador.TextChanged += (s, e) => BuscarTextChanged?.Invoke(this, EventArgs.Empty);
            chkEliminados.CheckedChanged += ChkEliminados_CheckedChanged;

            // 4. Vincular Evento de Selección de Tabla
            dgvInstitutos.CellClick += DgvInstitutos_CellClick;

            // 5. Estado Inicial de Controles y Botones
            ConfigurarEstadoBotones(EstadoFormulario.Inicial);
        }

        private void ConfigurarControles()
        {
            // ComboBox de Filtro de Búsqueda
            cmbBusqueda.Items.Clear();
            cmbBusqueda.Items.AddRange(new string[] { "Nombre", "Domicilio", "CodigoPostal" });
            cmbBusqueda.SelectedIndex = 0;
            cmbBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;

            // Configuración del DataGridView
            dgvInstitutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInstitutos.ReadOnly = true;
            dgvInstitutos.AllowUserToAddRows = false;
        }

        // ==========================================
        // PROPIEDADES DE LA INTERFAZ
        // ==========================================

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int InstitutoId { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Nombre { get => txtNombre.Text; set => txtNombre.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Domicilio { get => txtDomicilio.Text; set => txtDomicilio.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CodigoPostal { get => txtCodigoPostal.Text; set => txtCodigoPostal.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Referencia { get => txtReferencia.Text; set => txtReferencia.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ColumnaBusqueda => cmbBusqueda.Text;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TextoBusqueda => txtBuscador.Text;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool MostrarEliminados => chkEliminados.Checked;

        // Eventos
        public event EventHandler? NuevoClicked;
        public event EventHandler? EditarClicked;
        public event EventHandler? CancelarClicked;
        public event EventHandler? EliminarClicked;
        public event EventHandler? BuscarTextChanged;
        public event EventHandler? RestaurarClicked;

        // ==========================================
        // MÉTODOS DE LA INTERFAZ Y CONTROL DE UX
        // ==========================================

        public void CargarListaInstitutos(IEnumerable<Instituto> institutos)
        {
            dgvInstitutos.DataSource = institutos;

            // 1. Ocultar todas las columnas por defecto
            foreach (DataGridViewColumn col in dgvInstitutos.Columns)
            {
                col.Visible = false;
            }

            // 2. Mapeo de columnas visibles con su título, proporción (FillWeight) y orden
            var columnasVisibles = new (string Campo, string Titulo, float Peso)[]
            {
                ("Nombre",       "Nombre Instituto", 35f),
                ("Domicilio",    "Domicilio",        35f),
                ("CodigoPostal", "C.P.",             10f),
                ("Referencia",   "Referencia",       20f)
            };

            // 3. Aplicar configuración estandarizada en un solo ciclo
            for (int i = 0; i < columnasVisibles.Length; i++)
            {
                var (campo, titulo, peso) = columnasVisibles[i];

                if (dgvInstitutos.Columns[campo] is DataGridViewColumn col)
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
            MessageBox.Show(mensaje, "Gestión de Institutos", MessageBoxButtons.OK, icono);
        }

        public void LimpiarCampos()
        {
            InstitutoId = 0;
            txtNombre.Clear();
            txtDomicilio.Clear();
            txtCodigoPostal.Clear();
            txtReferencia.Clear();
        }

        public void HabilitarCampos(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            txtDomicilio.Enabled = habilitar;
            txtCodigoPostal.Enabled = habilitar;
            txtReferencia.Enabled = habilitar;
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

        private void DgvInstitutos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 1. Limpiar estados y campos ANTES de cargar los nuevos datos
                _presenter.CancelarModos();

                // 2. Extraer la fila seleccionada
                DataGridViewRow fila = dgvInstitutos.Rows[e.RowIndex];

                // 3. Asignar los datos a los controles
                InstitutoId = Convert.ToInt32(fila.Cells["Id"].Value);
                Nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";
                Domicilio = fila.Cells["Domicilio"].Value?.ToString() ?? "";
                CodigoPostal = fila.Cells["CodigoPostal"].Value?.ToString() ?? "";
                Referencia = fila.Cells["Referencia"].Value?.ToString() ?? "";

                // 4. Cambiar el estado visual para habilitar Editar/Eliminar
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