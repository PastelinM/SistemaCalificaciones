using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Data.Repositories;
using SistemaCalificaciones.UI.Presenters;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SistemaCalificaciones.UI.Views
{
    public partial class FormAlumnos : Form, IFormAlumnosView
    {
        private readonly FormAlumnosPresenter _presenter;

        public FormAlumnos()
        {
            InitializeComponent();
            ConfigurarControles();

            // 1. Instanciar capas MVP
            var alumnoRepo = new AlumnoRepository();
            var institutoRepo = new InstitutosRepository();
            _presenter = new FormAlumnosPresenter(this, alumnoRepo, institutoRepo);

            // 2. Vincular Botones con Eventos de la Interfaz
            btnNuevo.Click += (s, e) => NuevoClicked?.Invoke(this, EventArgs.Empty);
            btnEditar.Click += (s, e) => EditarClicked?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => CancelarClicked?.Invoke(this, EventArgs.Empty);
            btnEliminar.Click += (s, e) => EliminarClicked?.Invoke(this, EventArgs.Empty);

            // 3. Vincular Búsqueda y Filtros Dinámicos
            txtBuscador.TextChanged += (s, e) => BuscarTextChanged?.Invoke(this, EventArgs.Empty);
            chkEliminados.CheckedChanged += ChkEliminados_CheckedChanged;

            // 4. Vincular Evento de Selección de Tabla
            dgvAlumnos.CellClick += DgvAlumnos_CellClick;

            // 5. Estado Inicial de Controles y Botones
            ConfigurarEstadoBotones(EstadoFormulario.Inicial);
        }

        private void ConfigurarControles()
        {
            // ComboBox de Filtro de Búsqueda
            cmbBusqueda.Items.Clear();
            cmbBusqueda.Items.AddRange(new string[] { "Matricula", "Nombre", "ApPaterno" });
            cmbBusqueda.SelectedIndex = 1;
            cmbBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;

            // ComboBox Género
            cmbGenero.Items.Clear();
            cmbGenero.Items.AddRange(new string[] { "Masculino", "Femenino" });
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGenero.SelectedIndex = -1;

            // ComboBox Grado
            cmbGrado.Items.Clear();
            cmbGrado.Items.AddRange(new string[] { "1", "2", "3", "4", "5", "6" });
            cmbGrado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGrado.SelectedIndex = -1;

            // ComboBox Grupo
            cmbGrupo.Items.Clear();
            cmbGrupo.Items.AddRange(new string[] { "A", "B", "C", "D", "E", "F" });
            cmbGrupo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGrupo.SelectedIndex = -1;

            // Configuración del DataGridView
            dgvAlumnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAlumnos.ReadOnly = true;
            dgvAlumnos.AllowUserToAddRows = false;
        }

        // ==========================================
        // PROPIEDADES DE LA INTERFAZ
        // ==========================================

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int AlumnoId { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int InstitutoId
        {
            get => cmbInstituto.SelectedValue != null ? Convert.ToInt32(cmbInstituto.SelectedValue) : 0;
            set => cmbInstituto.SelectedValue = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Matricula { get => txtMatricula.Text; set => txtMatricula.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Nombre { get => txtNombre.Text; set => txtNombre.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ApPaterno { get => txtPaterno.Text; set => txtPaterno.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ApMaterno { get => txtMaterno.Text; set => txtMaterno.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Genero { get => cmbGenero.Text; set => cmbGenero.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Grado { get => cmbGrado.Text; set => cmbGrado.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Grupo { get => cmbGrupo.Text; set => cmbGrupo.Text = value; }

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

        public void CargarInstitutos(object institutos)
        {
            cmbInstituto.DataSource = institutos;
            cmbInstituto.DisplayMember = "Nombre";
            cmbInstituto.ValueMember = "Id";
            cmbInstituto.SelectedIndex = -1;
        }

        public void CargarListaAlumnos(object alumnos)
        {
            dgvAlumnos.DataSource = alumnos;

            // 1. Ocultar todas las columnas por defecto para evitar mantener listas de columnas a ocultar
            foreach (DataGridViewColumn col in dgvAlumnos.Columns)
            {
                col.Visible = false;
            }

            // 2. Mapeo de columnas visibles con su título, proporción (FillWeight) y orden
            var columnasVisibles = new (string Campo, string Titulo, float Peso)[]
            {
                ("Matricula", "Matrícula", 15f),
                ("Nombre",    "Nombre",    30f),
                ("ApPaterno", "A.Paterno",30f),
                ("Grado",     "Grado",     12f),
                ("Grupo",     "Grupo",     13f)
            };

            // 3. Aplicar configuración estandarizada en un solo ciclo
            for (int i = 0; i < columnasVisibles.Length; i++)
            {
                var (campo, titulo, peso) = columnasVisibles[i];

                if (dgvAlumnos.Columns[campo] is DataGridViewColumn col)
                {
                    col.Visible = true;
                    col.HeaderText = titulo;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    col.FillWeight = peso; // Proporción del espacio sobrante
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
            MessageBox.Show(mensaje, "Gestión de Alumnos", MessageBoxButtons.OK, icono);
        }

        public void LimpiarCampos()
        {
            AlumnoId = 0;
            txtMatricula.Clear();
            txtNombre.Clear();
            txtPaterno.Clear();
            txtMaterno.Clear();
            cmbGenero.SelectedIndex = -1;
            cmbGrado.SelectedIndex = -1;
            cmbGrupo.SelectedIndex = -1;
            cmbInstituto.SelectedIndex = -1;
        }

        public void HabilitarCampos(bool habilitar)
        {
            txtMatricula.Enabled = habilitar;
            txtNombre.Enabled = habilitar;
            txtPaterno.Enabled = habilitar;
            txtMaterno.Enabled = habilitar;
            cmbGenero.Enabled = habilitar;
            cmbGrado.Enabled = habilitar;
            cmbGrupo.Enabled = habilitar;
            cmbInstituto.Enabled = habilitar;
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

        private void DgvAlumnos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvAlumnos.Rows[e.RowIndex];

                AlumnoId = Convert.ToInt32(fila.Cells["Id"].Value);
                Matricula = fila.Cells["Matricula"].Value?.ToString() ?? "";
                Nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";
                ApPaterno = fila.Cells["ApPaterno"].Value?.ToString() ?? "";
                ApMaterno = fila.Cells["ApMaterno"].Value?.ToString() ?? "";
                Genero = fila.Cells["Genero"].Value?.ToString() ?? "";
                Grado = fila.Cells["Grado"].Value?.ToString() ?? "";
                Grupo = fila.Cells["Grupo"].Value?.ToString() ?? "";

                if (fila.Cells["InstitutoId"].Value != null)
                    cmbInstituto.SelectedValue = fila.Cells["InstitutoId"].Value;

                // Reiniciar banderas de edición/nuevo en el Presenter
                _presenter.CancelarModos();

                // Cambiar estado visual: Nuevo queda deshabilitado
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