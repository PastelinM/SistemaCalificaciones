using SistemaCalificaciones.Core.interfaces;
using SistemaCalificaciones.Data.Repositories;
using SistemaCalificaciones.UI.Presenters;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SistemaCalificaciones.UI.Views
{
    public partial class FormCiclos : Form, IFormCiclosView
    {
        private readonly FormCiclosPresenter _presenter;

        public FormCiclos()
        {
            InitializeComponent();
            ConfigurarControles();

            // 1. Instanciar capas MVP
            var cicloRepo = new CiclosRepository();
            _presenter = new FormCiclosPresenter(this, cicloRepo);

            // 2. Vincular Botones con Eventos de la Interfaz
            btnNuevo.Click += (s, e) => NuevoClicked?.Invoke(this, EventArgs.Empty);
            btnEditar.Click += (s, e) => EditarClicked?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => CancelarClicked?.Invoke(this, EventArgs.Empty);
            btnEliminar.Click += (s, e) => EliminarClicked?.Invoke(this, EventArgs.Empty);

            // 3. Vincular Búsqueda y Filtros Dinámicos
            txtBuscador.TextChanged += (s, e) => BuscarTextChanged?.Invoke(this, EventArgs.Empty);
            chkEliminados.CheckedChanged += ChkEliminados_CheckedChanged;

            // 4. Vincular Evento de Selección de Tabla
            dgvCiclos.CellClick += DgvCiclos_CellClick;

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
            dgvCiclos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCiclos.ReadOnly = true;
            dgvCiclos.AllowUserToAddRows = false;
        }

        // ==========================================
        // PROPIEDADES DE LA INTERFAZ
        // ==========================================
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CicloId { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Nombre { get => txtCiclo.Text; set => txtCiclo.Text = value; }

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
        public void CargarListaCiclosEscolares(object ciclos)
        {
            dgvCiclos.DataSource = ciclos;

            // 1. Ocultar todas las columnas por defecto para evitar columnas no deseadas
            foreach (DataGridViewColumn col in dgvCiclos.Columns)
            {
                col.Visible = false;
            }

            // 2. Definición estructurada: Campo, Título, Modo de Tamaño, Ancho Fijo, Formato
            var columnasVisibles = new (string Campo, string Titulo, DataGridViewAutoSizeColumnMode Modo, int Ancho, string? Formato)[]
            {
                ("Nombre", "Ciclo Escolar", DataGridViewAutoSizeColumnMode.Fill, 0, null)
            };

            // 3. Aplicar configuración en un solo ciclo estandarizado
            for (int i = 0; i < columnasVisibles.Length; i++)
            {
                var (campo, titulo, modo, ancho, formato) = columnasVisibles[i];

                if (dgvCiclos.Columns[campo] is DataGridViewColumn col)
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
            MessageBox.Show(mensaje, "Gestión de Ciclos Escolares", MessageBoxButtons.OK, icono);
        }

        public void LimpiarCampos()
        {
            CicloId = 0;
            txtCiclo.Clear();
        }

        public void HabilitarCampos(bool habilitar)
        {
            txtCiclo.Enabled = habilitar;
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
        private void DgvCiclos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 1. Primero reiniciamos el estado de edición en el Presenter (esto limpiará campos previo a la carga)
                _presenter.CancelarModos();

                // 2. Leemos la fila seleccionada
                DataGridViewRow fila = dgvCiclos.Rows[e.RowIndex];

                // 3. Asignamos los datos a los controles
                CicloId = Convert.ToInt32(fila.Cells["Id"].Value);
                Nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";

                // 4. Activamos el estado visual de fila seleccionada para habilitar los botones Editar/Eliminar
                ConfigurarEstadoBotones(EstadoFormulario.FilaSeleccionada);
            }
        }

        // 4. Activamos el estado visual de fila seleccionada para habilitar los botones Editar/Eliminar

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

        private void FormCiclos_Load(object sender, EventArgs e)
        {
        }
    }
}