using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Data.Repositories;
using SistemaCalificaciones.Presenters;
using SistemaCalificaciones.UI.Presenters;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SistemaCalificaciones.UI.Views
{
    public partial class FormCategorias : Form, IFormCategoriasView
    {
        private readonly FormCategoriasPresenter _presenter;

        public FormCategorias()
        {
            InitializeComponent();
            ConfigurarControles();

            // 1. Instanciar capas MVP
            var categoriaRepo = new CategoriaRepository();
            _presenter = new FormCategoriasPresenter(this, categoriaRepo);

            // 2. Vincular Botones con Eventos de la Interfaz
            btnNuevo.Click += (s, e) => NuevoClicked?.Invoke(this, EventArgs.Empty);
            btnEditar.Click += (s, e) => EditarClicked?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => CancelarClicked?.Invoke(this, EventArgs.Empty);
            btnEliminar.Click += (s, e) => EliminarClicked?.Invoke(this, EventArgs.Empty);

            // 3. Vincular Búsqueda y Filtros Dinámicos
            txtBuscador.TextChanged += (s, e) => BuscarTextChanged?.Invoke(this, EventArgs.Empty);
            chkEliminados.CheckedChanged += ChkEliminados_CheckedChanged;

            // 4. Vincular Evento de Selección de Tabla
            dgvCategorias.CellClick += DgvCategorias_CellClick;

            // 5. Estado Inicial de Controles y Botones
            ConfigurarEstadoBotones(EstadoFormulario.Inicial);
        }

        private void ConfigurarControles()
        {
            // ComboBox de Filtro de Búsqueda
            cmbBusqueda.Items.Clear();
            cmbBusqueda.Items.AddRange(new string[] { "Nombre", "Porcentaje" });
            cmbBusqueda.SelectedIndex = 0;
            cmbBusqueda.DropDownStyle = ComboBoxStyle.DropDownList;

            // Configuración del DataGridView
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.ReadOnly = true;
            dgvCategorias.AllowUserToAddRows = false;
        }

        // ==========================================
        // PROPIEDADES DE LA INTERFAZ
        // ==========================================

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CategoriaId { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Nombre { get => txtCategoria.Text; set => txtCategoria.Text = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Porcentaje { get => txtPorcentaje.Text; set => txtPorcentaje.Text = value; }

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

        public void CargarListaCategorias(object categorias)
        {
            dgvCategorias.DataSource = categorias;

            // 1. Ocultar todas las columnas por defecto para evitar columnas no deseadas
            foreach (DataGridViewColumn col in dgvCategorias.Columns)
            {
                col.Visible = false;
            }

            // 2. Definición estructurada: Campo, Título, Modo de Tamaño, Ancho Fijo, Formato
            var columnasVisibles = new (string Campo, string Titulo, DataGridViewAutoSizeColumnMode Modo, int Ancho, string? Formato)[]
            {
                ("Nombre",     "Categoría",  DataGridViewAutoSizeColumnMode.Fill,   0,   null),
                ("Porcentaje", "Porcentaje", DataGridViewAutoSizeColumnMode.None, 110, "N2")
            };

            // 3. Aplicar configuración en un solo ciclo estandarizado
            for (int i = 0; i < columnasVisibles.Length; i++)
            {
                var (campo, titulo, modo, ancho, formato) = columnasVisibles[i];

                if (dgvCategorias.Columns[campo] is DataGridViewColumn col)
                {
                    col.Visible = true;
                    col.HeaderText = titulo;
                    col.DisplayIndex = i;
                    col.AutoSizeMode = modo;

                    if (modo != DataGridViewAutoSizeColumnMode.Fill)
                        col.Width = ancho;

                    if (!string.IsNullOrEmpty(formato))
                        col.DefaultCellStyle.Format = formato;
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
            MessageBox.Show(mensaje, "Gestión de Categorías", MessageBoxButtons.OK, icono);
        }

        public void LimpiarCampos()
        {
            CategoriaId = 0;
            txtCategoria.Clear();
            txtPorcentaje.Clear();
        }

        public void HabilitarCampos(bool habilitar)
        {
            txtCategoria.Enabled = habilitar;
            txtPorcentaje.Enabled = habilitar;
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

        private void DgvCategorias_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvCategorias.Rows[e.RowIndex];

                CategoriaId = Convert.ToInt32(fila.Cells["Id"].Value);
                Nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";
                Porcentaje = fila.Cells["Porcentaje"].Value?.ToString() ?? "";

                // Reiniciar banderas de edición/nuevo en el Presenter
                _presenter.CancelarModos();

                // Cambiar estado visual
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

        private void FormCategorias_Load(object sender, EventArgs e)
        {

        }
    }
}