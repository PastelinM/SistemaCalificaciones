namespace SistemaCalificaciones.UI.Views
{
    partial class FormActividades
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
            dgvActividades = new DataGridView();
            chkEliminados = new CheckBox();
            cmbBusqueda = new ComboBox();
            lblTotal = new Label();
            btnEliminar = new Button();
            txtBuscador = new TextBox();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            btnEditar = new Button();
            label4 = new Label();
            label3 = new Label();
            dtpFecha = new DateTimePicker();
            txtPuntaje = new TextBox();
            label2 = new Label();
            txtDescripcion = new RichTextBox();
            label1 = new Label();
            txtTitulo = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            btnCancelar = new Button();
            btnNuevo = new Button();
            cmbCiclo = new ComboBox();
            cmbMateria = new ComboBox();
            cmbPeriodo = new ComboBox();
            cmbCategoria = new ComboBox();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvActividades).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvActividades
            // 
            dgvActividades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvActividades.Location = new Point(17, 118);
            dgvActividades.Name = "dgvActividades";
            dgvActividades.Size = new Size(642, 462);
            dgvActividades.TabIndex = 15;
            // 
            // chkEliminados
            // 
            chkEliminados.AutoSize = true;
            chkEliminados.Location = new Point(549, 46);
            chkEliminados.Name = "chkEliminados";
            chkEliminados.Size = new Size(123, 29);
            chkEliminados.TabIndex = 14;
            chkEliminados.Text = "Eliminados";
            chkEliminados.UseVisualStyleBackColor = true;
            // 
            // cmbBusqueda
            // 
            cmbBusqueda.FormattingEnabled = true;
            cmbBusqueda.Location = new Point(17, 44);
            cmbBusqueda.Name = "cmbBusqueda";
            cmbBusqueda.Size = new Size(109, 33);
            cmbBusqueda.TabIndex = 12;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(586, 626);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(56, 25);
            lblTotal.TabIndex = 28;
            lblTotal.Text = "Total:";
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.IndianRed;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(17, 614);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(143, 49);
            btnEliminar.TabIndex = 16;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // txtBuscador
            // 
            txtBuscador.Location = new Point(132, 44);
            txtBuscador.Name = "txtBuscador";
            txtBuscador.Size = new Size(399, 33);
            txtBuscador.TabIndex = 13;
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
            splitContainer1.Size = new Size(1184, 693);
            splitContainer1.SplitterDistance = 474;
            splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnEditar);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(dtpFecha);
            groupBox1.Controls.Add(txtPuntaje);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtDescripcion);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtTitulo);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(btnCancelar);
            groupBox1.Controls.Add(btnNuevo);
            groupBox1.Controls.Add(cmbCiclo);
            groupBox1.Controls.Add(cmbMateria);
            groupBox1.Controls.Add(cmbPeriodo);
            groupBox1.Controls.Add(cmbCategoria);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(4, 10);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(466, 671);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos de Actividades";
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.Orange;
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(172, 616);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(140, 49);
            btnEditar.TabIndex = 10;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(16, 527);
            label4.Name = "label4";
            label4.Size = new Size(139, 25);
            label4.TabIndex = 47;
            label4.Text = "Fecha Entrega*";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(16, 455);
            label3.Name = "label3";
            label3.Size = new Size(84, 25);
            label3.TabIndex = 46;
            label3.Text = "Puntaje*";
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(16, 559);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(439, 33);
            dtpFecha.TabIndex = 8;
            // 
            // txtPuntaje
            // 
            txtPuntaje.Location = new Point(16, 483);
            txtPuntaje.Name = "txtPuntaje";
            txtPuntaje.Size = new Size(439, 33);
            txtPuntaje.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(16, 366);
            label2.Name = "label2";
            label2.Size = new Size(119, 25);
            label2.TabIndex = 45;
            label2.Text = "Descripción*";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(16, 394);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(444, 48);
            txtDescripcion.TabIndex = 6;
            txtDescripcion.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(16, 302);
            label1.Name = "label1";
            label1.Size = new Size(68, 25);
            label1.TabIndex = 44;
            label1.Text = "Titulo*";
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(16, 330);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(444, 33);
            txtTitulo.TabIndex = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(16, 238);
            label8.Name = "label8";
            label8.Size = new Size(127, 25);
            label8.TabIndex = 43;
            label8.Text = "Ciclo Escolar*";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(16, 174);
            label7.Name = "label7";
            label7.Size = new Size(85, 25);
            label7.TabIndex = 42;
            label7.Text = "Materia*";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(16, 110);
            label6.Name = "label6";
            label6.Size = new Size(85, 25);
            label6.TabIndex = 41;
            label6.Text = "Periodo*";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(16, 46);
            label5.Name = "label5";
            label5.Size = new Size(102, 25);
            label5.TabIndex = 40;
            label5.Text = "Categoria*";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(318, 616);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(142, 49);
            btnCancelar.TabIndex = 11;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.DodgerBlue;
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(21, 616);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(140, 49);
            btnNuevo.TabIndex = 9;
            btnNuevo.Text = "+Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            // 
            // cmbCiclo
            // 
            cmbCiclo.FormattingEnabled = true;
            cmbCiclo.Location = new Point(16, 266);
            cmbCiclo.Name = "cmbCiclo";
            cmbCiclo.Size = new Size(444, 33);
            cmbCiclo.TabIndex = 4;
            // 
            // cmbMateria
            // 
            cmbMateria.FormattingEnabled = true;
            cmbMateria.Location = new Point(16, 202);
            cmbMateria.Name = "cmbMateria";
            cmbMateria.Size = new Size(444, 33);
            cmbMateria.TabIndex = 3;
            // 
            // cmbPeriodo
            // 
            cmbPeriodo.FormattingEnabled = true;
            cmbPeriodo.Location = new Point(16, 138);
            cmbPeriodo.Name = "cmbPeriodo";
            cmbPeriodo.Size = new Size(444, 33);
            cmbPeriodo.TabIndex = 2;
            // 
            // cmbCategoria
            // 
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(16, 74);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(444, 33);
            cmbCategoria.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblTotal);
            groupBox2.Controls.Add(btnEliminar);
            groupBox2.Controls.Add(dgvActividades);
            groupBox2.Controls.Add(chkEliminados);
            groupBox2.Controls.Add(txtBuscador);
            groupBox2.Controls.Add(cmbBusqueda);
            groupBox2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(21, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(673, 669);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Listado de Actividades";
            // 
            // FormActividades
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 693);
            Controls.Add(splitContainer1);
            Name = "FormActividades";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)dgvActividades).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvActividades;
        private CheckBox chkEliminados;
        private ComboBox cmbBusqueda;
        private Label lblTotal;
        private Button btnEliminar;
        private TextBox txtBuscador;
        private SplitContainer splitContainer1;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Button btnCancelar;
        private Button btnNuevo;
        private ComboBox cmbCiclo;
        private ComboBox cmbMateria;
        private ComboBox cmbPeriodo;
        private ComboBox cmbCategoria;
        private DateTimePicker dtpFecha;
        private TextBox txtPuntaje;
        private Label label2;
        private RichTextBox txtDescripcion;
        private Label label1;
        private TextBox txtTitulo;
        private Label label4;
        private Label label3;
        private Button btnEditar;
    }
}