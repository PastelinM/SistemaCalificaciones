namespace SistemaCalificaciones.UI.Views
{
    partial class FormCategorias
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            btnCancelar = new Button();
            btnEditar = new Button();
            btnNuevo = new Button();
            txtPorcentaje = new TextBox();
            txtCategoria = new TextBox();
            groupBox2 = new GroupBox();
            lblTotal = new Label();
            btnEliminar = new Button();
            dgvCategorias = new DataGridView();
            chkEliminados = new CheckBox();
            txtBuscador = new TextBox();
            cmbBusqueda = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Size = new Size(1184, 563);
            splitContainer1.SplitterDistance = 490;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnCancelar);
            groupBox1.Controls.Add(btnEditar);
            groupBox1.Controls.Add(btnNuevo);
            groupBox1.Controls.Add(txtPorcentaje);
            groupBox1.Controls.Add(txtCategoria);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(17, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(457, 539);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos de Categoria";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 225);
            label2.Name = "label2";
            label2.Size = new Size(109, 25);
            label2.TabIndex = 28;
            label2.Text = "Porcentaje*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 131);
            label1.Name = "label1";
            label1.Size = new Size(102, 25);
            label1.TabIndex = 27;
            label1.Text = "Categoria*";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(308, 471);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(143, 49);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.Orange;
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(159, 471);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(143, 49);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = SystemColors.Highlight;
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(6, 471);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(140, 49);
            btnNuevo.TabIndex = 2;
            btnNuevo.Text = "+Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            // 
            // txtPorcentaje
            // 
            txtPorcentaje.Location = new Point(23, 253);
            txtPorcentaje.Name = "txtPorcentaje";
            txtPorcentaje.Size = new Size(411, 33);
            txtPorcentaje.TabIndex = 1;
            // 
            // txtCategoria
            // 
            txtCategoria.Location = new Point(23, 169);
            txtCategoria.Name = "txtCategoria";
            txtCategoria.Size = new Size(411, 33);
            txtCategoria.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblTotal);
            groupBox2.Controls.Add(btnEliminar);
            groupBox2.Controls.Add(dgvCategorias);
            groupBox2.Controls.Add(chkEliminados);
            groupBox2.Controls.Add(txtBuscador);
            groupBox2.Controls.Add(cmbBusqueda);
            groupBox2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(3, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(675, 539);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Listado de Categorias";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(593, 495);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(66, 25);
            lblTotal.TabIndex = 28;
            lblTotal.Text = "Total:1";
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.IndianRed;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(17, 471);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(143, 49);
            btnEliminar.TabIndex = 9;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // dgvCategorias
            // 
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategorias.Location = new Point(17, 118);
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.Size = new Size(642, 317);
            dgvCategorias.TabIndex = 8;
            // 
            // chkEliminados
            // 
            chkEliminados.AutoSize = true;
            chkEliminados.Location = new Point(549, 46);
            chkEliminados.Name = "chkEliminados";
            chkEliminados.Size = new Size(123, 29);
            chkEliminados.TabIndex = 7;
            chkEliminados.Text = "Eliminados";
            chkEliminados.UseVisualStyleBackColor = true;
            // 
            // txtBuscador
            // 
            txtBuscador.Location = new Point(144, 44);
            txtBuscador.Name = "txtBuscador";
            txtBuscador.Size = new Size(399, 33);
            txtBuscador.TabIndex = 6;
            // 
            // cmbBusqueda
            // 
            cmbBusqueda.FormattingEnabled = true;
            cmbBusqueda.Location = new Point(17, 44);
            cmbBusqueda.Name = "cmbBusqueda";
            cmbBusqueda.Size = new Size(109, 33);
            cmbBusqueda.TabIndex = 5;
            // 
            // FormCategorias
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 563);
            Controls.Add(splitContainer1);
            MaximizeBox = false;
            Name = "FormCategorias";
            StartPosition = FormStartPosition.CenterScreen;
            Load += FormCategorias_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private Button btnNuevo;
        private TextBox txtPorcentaje;
        private TextBox txtCategoria;
        private GroupBox groupBox2;
        private Button btnEditar;
        private Button btnCancelar;
        private DataGridView dgvCategorias;
        private CheckBox chkEliminados;
        private TextBox txtBuscador;
        private ComboBox cmbBusqueda;
        private Label label1;
        private Label lblTotal;
        private Button btnEliminar;
        private Label label2;
    }
}