using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Data.Repositories;
using SistemaCalificaciones.UI.Presenters;
using SistemaCalificaciones.Core.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SistemaCalificaciones.UI.Views
{
    public partial class FormActividades : Form, IFormActividadesView
    {
        private readonly FormActividadesPresenter _presenter;

        public FormActividades()
        {
            InitializeComponent();
            ConfigurarControles();

            // 1. Instanciar capas MVP con los catálogos requeridos
            var actividadRepo = new ActividadRepository();
            var categoriaRepo = new CategoriaRepository();
            var periodoRepo = new PeriodoRepository();
            var materiaRepo = new MateriaRepository();
            var cicloRepo = new CiclosRepository();

            _presenter = new FormActividadesPresenter(this, actividadRepo, categoriaRepo, periodoRepo, materiaRepo, cicloRepo);

            // 2. Vincular Botones con Eventos de la Interfaz
            btnNuevo.Click += (s, e) => NuevoClicked?.Invoke(this, EventArgs.Empty);
            btnEditar.Click += (s, e) => EditarClicked?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => CancelarClicked?.Invoke(this, EventArgs.Empty);
            btnEliminar.Click += (s, e) => EliminarClicked?.Invoke(this, EventArgs.Empty);

            // 3. Vincular Búsqueda y Filtros Dinámicos
            txtBuscador.TextChanged += (s, e) => BuscarTextChanged?.Invoke(this, EventArgs.Empty);
            chkEliminados.CheckedChanged += ChkEliminados_CheckedChanged;

            // 4. Vincular Evento de Selección de Tabla
            dgvActividades.CellClick += DgvActividades_CellClick;

            // 5. Estado Inicial de Controles y Botones
            ConfigurarEstadoBotones(EstadoFormulario.Inicial);
        }

        private void ConfigurarControles()
        {
            // ComboBox de Filtro de Búsqueda
            cmbBusqueda.Items.Clear();
            cmbBusqueda.Items.AddRange(new string[] { "Titulo", "Descripcion" });
            cmbBusqueda.SelectedIndex = 0;
            cmbBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;

            // Configuración del DataGridView
            dgvActividades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActividades.ReadOnly = true;
            dgvActividades.AllowUserToAddRows = false;
        }

        // ==========================================
        // PROPIEDADES DE LA INTERFAZ
        // ==========================================

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Id { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CategoriaId
        {
            get => cmbCategoria.SelectedValue != null ? Convert.ToInt32(cmbCategoria.SelectedValue) : 0;
            set => cmbCategoria.SelectedValue = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PeriodoId
        {
            get => cmbPeriodo.SelectedValue != null ? Convert.ToInt32(cmbPeriodo.SelectedValue) : 0;
            set => cmbPeriodo.SelectedValue = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MateriaId
        {
            get => cmbMateria.SelectedValue != null ? Convert.ToInt32(cmbMateria.SelectedValue) : 0;
            set => cmbMateria.SelectedValue = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CicloEscolarId
        {
            get => cmbCiclo.SelectedValue != null ? Convert.ToInt32(cmbCiclo.SelectedValue) : 0;
            set => cmbCiclo.SelectedValue = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Titulo { get => txtTitulo.Text; set => txtTitulo.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string? Descripcion { get => txtDescripcion.Text; set => txtDescripcion.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal PuntajeMaximo
        {
            get => decimal.TryParse(txtPuntaje.Text, out decimal val) ? val : 0m;
            set => txtPuntaje.Text = value.ToString("0.00");
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? FechaEntrega
        {
            get => dtpFecha.Value;
            set => dtpFecha.Value = value ?? DateTime.Now;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool? Estatus { get; set; } = true;

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

        public void CargarCategorias(object categorias)
        {
            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "Id";
            cmbCategoria.SelectedIndex = -1;
        }

        public void CargarPeriodos(object periodos)
        {
            cmbPeriodo.DataSource = periodos;
            cmbPeriodo.DisplayMember = "Nombre";
            cmbPeriodo.ValueMember = "Id";
            cmbPeriodo.SelectedIndex = -1;
        }

        public void CargarMaterias(object materias)
        {
            cmbMateria.DataSource = materias;
            cmbMateria.DisplayMember = "Nombre";
            cmbMateria.ValueMember = "Id";
            cmbMateria.SelectedIndex = -1;
        }

        public void CargarCiclosEscolares(object ciclos)
        {
            cmbCiclo.DataSource = ciclos;
            cmbCiclo.DisplayMember = "Nombre";
            cmbCiclo.ValueMember = "Id";
            cmbCiclo.SelectedIndex = -1;
        }

        public void CargarListaActividades(object actividades)
        {
            dgvActividades.DataSource = actividades;

            foreach (DataGridViewColumn col in dgvActividades.Columns)
            {
                col.Visible = false;
            }

            var columnasVisibles = new (string Campo, string Titulo, float Peso)[]
            {
                ("Titulo", "Título", 40f),
                ("PuntajeMaximo", "Puntaje", 15f),
                ("FechaEntrega", "Fecha ", 25f),
                ("Estatus", "Activo", 10f)
            };

            for (int i = 0; i < columnasVisibles.Length; i++)
            {
                var (campo, titulo, peso) = columnasVisibles[i];

                if (dgvActividades.Columns[campo] is DataGridViewColumn col)
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
            MessageBox.Show(mensaje, "Gestión de Actividades", MessageBoxButtons.OK, icono);
        }

        public void LimpiarCampos()
        {
            Id = 0;
            txtTitulo.Clear();
            txtDescripcion.Clear();
            txtPuntaje.Clear();
            dtpFecha.Value = DateTime.Now;

            cmbCategoria.SelectedIndex = -1;
            cmbPeriodo.SelectedIndex = -1;
            cmbMateria.SelectedIndex = -1;
            cmbCiclo.SelectedIndex = -1;
        }

        public void HabilitarCampos(bool habilitar)
        {
            txtTitulo.Enabled = habilitar;
            txtDescripcion.Enabled = habilitar;
            txtPuntaje.Enabled = habilitar;
            dtpFecha.Enabled = habilitar;

            cmbCategoria.Enabled = habilitar;
            cmbPeriodo.Enabled = habilitar;
            cmbMateria.Enabled = habilitar;
            cmbCiclo.Enabled = habilitar;
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

        private void DgvActividades_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvActividades.Rows[e.RowIndex];

                Id = Convert.ToInt32(fila.Cells["Id"].Value);
                Titulo = fila.Cells["Titulo"].Value?.ToString() ?? "";
                Descripcion = fila.Cells["Descripcion"].Value?.ToString() ?? "";

                if (fila.Cells["PuntajeMaximo"].Value != null)
                    txtPuntaje.Text = fila.Cells["PuntajeMaximo"].Value.ToString();

                if (fila.Cells["FechaEntrega"].Value != null)
                    dtpFecha.Value = Convert.ToDateTime(fila.Cells["FechaEntrega"].Value);

                if (fila.Cells["CategoriaId"].Value != null)
                    cmbCategoria.SelectedValue = fila.Cells["CategoriaId"].Value;

                if (fila.Cells["PeriodoId"].Value != null)
                    cmbPeriodo.SelectedValue = fila.Cells["PeriodoId"].Value;

                if (fila.Cells["MateriaId"].Value != null)
                    cmbMateria.SelectedValue = fila.Cells["MateriaId"].Value;

                if (fila.Cells["CicloEscolarId"].Value != null)
                    cmbCiclo.SelectedValue = fila.Cells["CicloEscolarId"].Value;

                _presenter.CancelarModos();
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