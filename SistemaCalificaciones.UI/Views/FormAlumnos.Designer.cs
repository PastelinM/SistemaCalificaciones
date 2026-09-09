namespace SistemaCalificaciones.UI.Views
{
    partial class FormAlumnos
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
            btnEditar = new Button();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnCancelar = new Button();
            btnNuevo = new Button();
            cmbInstituto = new ComboBox();
            cmbGrupo = new ComboBox();
            cmbGrado = new ComboBox();
            cmbGenero = new ComboBox();
            txtMaterno = new TextBox();
            txtPaterno = new TextBox();
            txtNombre = new TextBox();
            txtMatricula = new TextBox();
            groupBox2 = new GroupBox();
            lblTotal = new Label();
            btnEliminar = new Button();
            dgvAlumnos = new DataGridView();
            chkEliminados = new CheckBox();
            txtBuscador = new TextBox();
            cmbBusqueda = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).BeginInit();
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
            splitContainer1.Size = new Size(1184, 661);
            splitContainer1.SplitterDistance = 474;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnEditar);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnCancelar);
            groupBox1.Controls.Add(btnNuevo);
            groupBox1.Controls.Add(cmbInstituto);
            groupBox1.Controls.Add(cmbGrupo);
            groupBox1.Controls.Add(cmbGrado);
            groupBox1.Controls.Add(cmbGenero);
            groupBox1.Controls.Add(txtMaterno);
            groupBox1.Controls.Add(txtPaterno);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(txtMatricula);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(4, 10);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(466, 640);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos de Alumnos";
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.Orange;
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(171, 585);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(140, 49);
            btnEditar.TabIndex = 9;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(16, 507);
            label8.Name = "label8";
            label8.Size = new Size(89, 25);
            label8.TabIndex = 28;
            label8.Text = "Instituto*";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(16, 444);
            label7.Name = "label7";
            label7.Size = new Size(73, 25);
            label7.TabIndex = 27;
            label7.Text = "Grupo*";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(16, 375);
            label6.Name = "label6";
            label6.Size = new Size(72, 25);
            label6.TabIndex = 26;
            label6.Text = "Grado*";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(16, 311);
            label5.Name = "label5";
            label5.Size = new Size(82, 25);
            label5.TabIndex = 25;
            label5.Text = "Genero*";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(16, 243);
            label4.Name = "label4";
            label4.Size = new Size(159, 25);
            label4.TabIndex = 24;
            label4.Text = "Apellido Materno";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(16, 178);
            label3.Name = "label3";
            label3.Size = new Size(160, 25);
            label3.TabIndex = 23;
            label3.Text = "Apellido Paterno*";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(16, 114);
            label2.Name = "label2";
            label2.Size = new Size(89, 25);
            label2.TabIndex = 22;
            label2.Text = "Nombre*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(16, 50);
            label1.Name = "label1";
            label1.Size = new Size(100, 25);
            label1.TabIndex = 21;
            label1.Text = "Matrícula*";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(318, 585);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(142, 49);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.DodgerBlue;
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(16, 585);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(140, 49);
            btnNuevo.TabIndex = 8;
            btnNuevo.Text = "+Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            // 
            // cmbInstituto
            // 
            cmbInstituto.FormattingEnabled = true;
            cmbInstituto.Location = new Point(16, 535);
            cmbInstituto.Name = "cmbInstituto";
            cmbInstituto.Size = new Size(444, 33);
            cmbInstituto.TabIndex = 7;
            // 
            // cmbGrupo
            // 
            cmbGrupo.FormattingEnabled = true;
            cmbGrupo.Location = new Point(16, 472);
            cmbGrupo.Name = "cmbGrupo";
            cmbGrupo.Size = new Size(444, 33);
            cmbGrupo.TabIndex = 6;
            // 
            // cmbGrado
            // 
            cmbGrado.FormattingEnabled = true;
            cmbGrado.Location = new Point(16, 403);
            cmbGrado.Name = "cmbGrado";
            cmbGrado.Size = new Size(444, 33);
            cmbGrado.TabIndex = 5;
            // 
            // cmbGenero
            // 
            cmbGenero.FormattingEnabled = true;
            cmbGenero.Location = new Point(16, 339);
            cmbGenero.Name = "cmbGenero";
            cmbGenero.Size = new Size(444, 33);
            cmbGenero.TabIndex = 4;
            // 
            // txtMaterno
            // 
            txtMaterno.Location = new Point(16, 271);
            txtMaterno.Name = "txtMaterno";
            txtMaterno.Size = new Size(444, 33);
            txtMaterno.TabIndex = 3;
            // 
            // txtPaterno
            // 
            txtPaterno.Location = new Point(16, 206);
            txtPaterno.Name = "txtPaterno";
            txtPaterno.Size = new Size(444, 33);
            txtPaterno.TabIndex = 2;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(16, 142);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(444, 33);
            txtNombre.TabIndex = 1;
            // 
            // txtMatricula
            // 
            txtMatricula.Location = new Point(16, 78);
            txtMatricula.Name = "txtMatricula";
            txtMatricula.Size = new Size(444, 33);
            txtMatricula.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblTotal);
            groupBox2.Controls.Add(btnEliminar);
            groupBox2.Controls.Add(dgvAlumnos);
            groupBox2.Controls.Add(chkEliminados);
            groupBox2.Controls.Add(txtBuscador);
            groupBox2.Controls.Add(cmbBusqueda);
            groupBox2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(21, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(673, 638);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Listado de Alumnos";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(588, 595);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(56, 25);
            lblTotal.TabIndex = 28;
            lblTotal.Text = "Total:";
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.IndianRed;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(17, 583);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(143, 49);
            btnEliminar.TabIndex = 15;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // dgvAlumnos
            // 
            dgvAlumnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlumnos.Location = new Point(17, 118);
            dgvAlumnos.Name = "dgvAlumnos";
            dgvAlumnos.Size = new Size(642, 432);
            dgvAlumnos.TabIndex = 14;
            // 
            // chkEliminados
            // 
            chkEliminados.AutoSize = true;
            chkEliminados.Location = new Point(549, 46);
            chkEliminados.Name = "chkEliminados";
            chkEliminados.Size = new Size(123, 29);
            chkEliminados.TabIndex = 13;
            chkEliminados.Text = "Eliminados";
            chkEliminados.UseVisualStyleBackColor = true;
            // 
            // txtBuscador
            // 
            txtBuscador.Location = new Point(132, 44);
            txtBuscador.Name = "txtBuscador";
            txtBuscador.Size = new Size(399, 33);
            txtBuscador.TabIndex = 12;
            // 
            // cmbBusqueda
            // 
            cmbBusqueda.FormattingEnabled = true;
            cmbBusqueda.Location = new Point(17, 44);
            cmbBusqueda.Name = "cmbBusqueda";
            cmbBusqueda.Size = new Size(109, 33);
            cmbBusqueda.TabIndex = 11;
            // 
            // FormAlumnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 661);
            Controls.Add(splitContainer1);
            MaximizeBox = false;
            Name = "FormAlumnos";
            StartPosition = FormStartPosition.CenterScreen;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnCancelar;
        private Button btnNuevo;
        private ComboBox cmbInstituto;
        private ComboBox cmbGrupo;
        private ComboBox cmbGrado;
        private ComboBox cmbGenero;
        private TextBox txtMaterno;
        private TextBox txtPaterno;
        private TextBox txtNombre;
        private TextBox txtMatricula;
        private Button btnEditar;
        private GroupBox groupBox2;
        private Label lblTotal;
        private Button btnEliminar;
        private DataGridView dgvAlumnos;
        private CheckBox chkEliminados;
        private TextBox txtBuscador;
        private ComboBox cmbBusqueda;
    }
}