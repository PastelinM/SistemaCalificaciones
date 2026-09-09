namespace SistemaCalificaciones.UI.Views
{
    partial class FormInstitutos
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
            groupBox2 = new GroupBox();
            lblTotal = new Label();
            btnEliminar = new Button();
            dgvInstitutos = new DataGridView();
            chkEliminados = new CheckBox();
            txtBuscador = new TextBox();
            cmbBusqueda = new ComboBox();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            txtReferencia = new RichTextBox();
            txtCodigoPostal = new TextBox();
            label2 = new Label();
            label1 = new Label();
            btnCancelar = new Button();
            btnEditar = new Button();
            btnNuevo = new Button();
            txtDomicilio = new TextBox();
            txtNombre = new TextBox();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInstitutos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblTotal);
            groupBox2.Controls.Add(btnEliminar);
            groupBox2.Controls.Add(dgvInstitutos);
            groupBox2.Controls.Add(chkEliminados);
            groupBox2.Controls.Add(txtBuscador);
            groupBox2.Controls.Add(cmbBusqueda);
            groupBox2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(3, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(675, 539);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Listado de Instituto";
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
            btnEliminar.TabIndex = 12;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // dgvInstitutos
            // 
            dgvInstitutos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInstitutos.Location = new Point(17, 118);
            dgvInstitutos.Name = "dgvInstitutos";
            dgvInstitutos.Size = new Size(642, 317);
            dgvInstitutos.TabIndex = 11;
            // 
            // chkEliminados
            // 
            chkEliminados.AutoSize = true;
            chkEliminados.Location = new Point(549, 46);
            chkEliminados.Name = "chkEliminados";
            chkEliminados.Size = new Size(123, 29);
            chkEliminados.TabIndex = 10;
            chkEliminados.Text = "Eliminados";
            chkEliminados.UseVisualStyleBackColor = true;
            // 
            // txtBuscador
            // 
            txtBuscador.Location = new Point(144, 44);
            txtBuscador.Name = "txtBuscador";
            txtBuscador.Size = new Size(399, 33);
            txtBuscador.TabIndex = 9;
            // 
            // cmbBusqueda
            // 
            cmbBusqueda.FormattingEnabled = true;
            cmbBusqueda.Location = new Point(17, 44);
            cmbBusqueda.Name = "cmbBusqueda";
            cmbBusqueda.Size = new Size(109, 33);
            cmbBusqueda.TabIndex = 0;
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
            splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtReferencia);
            groupBox1.Controls.Add(txtCodigoPostal);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnCancelar);
            groupBox1.Controls.Add(btnEditar);
            groupBox1.Controls.Add(btnNuevo);
            groupBox1.Controls.Add(txtDomicilio);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(17, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(457, 539);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos de Instituto";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 326);
            label4.Name = "label4";
            label4.Size = new Size(100, 25);
            label4.TabIndex = 32;
            label4.Text = "Referencia";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 228);
            label3.Name = "label3";
            label3.Size = new Size(136, 25);
            label3.TabIndex = 31;
            label3.Text = "Codígo Postal*";
            // 
            // txtReferencia
            // 
            txtReferencia.Location = new Point(29, 365);
            txtReferencia.Name = "txtReferencia";
            txtReferencia.Size = new Size(405, 70);
            txtReferencia.TabIndex = 4;
            txtReferencia.Text = "";
            // 
            // txtCodigoPostal
            // 
            txtCodigoPostal.Location = new Point(23, 265);
            txtCodigoPostal.Name = "txtCodigoPostal";
            txtCodigoPostal.Size = new Size(411, 33);
            txtCodigoPostal.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 151);
            label2.Name = "label2";
            label2.Size = new Size(100, 25);
            label2.TabIndex = 28;
            label2.Text = "Domicilio*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 68);
            label1.Name = "label1";
            label1.Size = new Size(89, 25);
            label1.TabIndex = 27;
            label1.Text = "Instituto*";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(308, 471);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(143, 49);
            btnCancelar.TabIndex = 7;
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
            btnEditar.TabIndex = 6;
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
            btnNuevo.TabIndex = 5;
            btnNuevo.Text = "+Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            // 
            // txtDomicilio
            // 
            txtDomicilio.Location = new Point(23, 179);
            txtDomicilio.Name = "txtDomicilio";
            txtDomicilio.Size = new Size(411, 33);
            txtDomicilio.TabIndex = 1;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(23, 96);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(411, 33);
            txtNombre.TabIndex = 0;
            // 
            // FormInstitutos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 563);
            Controls.Add(splitContainer1);
            MaximizeBox = false;
            Name = "FormInstitutos";
            StartPosition = FormStartPosition.CenterScreen;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInstitutos).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox2;
        private Label lblTotal;
        private Button btnEliminar;
        private DataGridView dgvInstitutos;
        private CheckBox chkEliminados;
        private TextBox txtBuscador;
        private ComboBox cmbBusqueda;
        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private Button btnCancelar;
        private Button btnEditar;
        private Button btnNuevo;
        private TextBox txtDomicilio;
        private TextBox txtNombre;
        private Label label4;
        private Label label3;
        private RichTextBox txtReferencia;
        private TextBox txtCodigoPostal;
    }
}