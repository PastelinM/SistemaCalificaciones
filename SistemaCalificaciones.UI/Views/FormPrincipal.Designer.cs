namespace SistemaCalificaciones.UI.Views
{
    partial class FormPrincipal
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
            components = new System.ComponentModel.Container();
            menuStrip = new MenuStrip();
            fileMenu = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            formulariosToolStripMenuItem = new ToolStripMenuItem();
            institutosToolStripMenuItem = new ToolStripMenuItem();
            categoriasToolStripMenuItem = new ToolStripMenuItem();
            periodosToolStripMenuItem = new ToolStripMenuItem();
            maToolStripMenuItem = new ToolStripMenuItem();
            cicloEscolarToolStripMenuItem = new ToolStripMenuItem();
            periodosToolStripMenuItem1 = new ToolStripMenuItem();
            consultasToolStripMenuItem = new ToolStripMenuItem();
            actividadesToolStripMenuItem = new ToolStripMenuItem();
            calificacionesToolStripMenuItem = new ToolStripMenuItem();
            reportesToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            toolTip = new ToolTip(components);
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, formulariosToolStripMenuItem, consultasToolStripMenuItem, reportesToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 2, 0, 2);
            menuStrip.Size = new Size(1318, 33);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileMenu.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fileMenu.ImageTransparentColor = SystemColors.ActiveBorder;
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new Size(88, 29);
            fileMenu.Text = "Archivo";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(114, 30);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += ExitToolsStripMenuItem_Click;
            // 
            // formulariosToolStripMenuItem
            // 
            formulariosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { institutosToolStripMenuItem, categoriasToolStripMenuItem, periodosToolStripMenuItem, maToolStripMenuItem, cicloEscolarToolStripMenuItem, periodosToolStripMenuItem1 });
            formulariosToolStripMenuItem.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            formulariosToolStripMenuItem.Name = "formulariosToolStripMenuItem";
            formulariosToolStripMenuItem.Size = new Size(108, 29);
            formulariosToolStripMenuItem.Text = "Catálogos";
            // 
            // institutosToolStripMenuItem
            // 
            institutosToolStripMenuItem.Name = "institutosToolStripMenuItem";
            institutosToolStripMenuItem.Size = new Size(191, 30);
            institutosToolStripMenuItem.Text = "Alumnos";
            institutosToolStripMenuItem.Click += institutosToolStripMenuItem_Click;
            // 
            // categoriasToolStripMenuItem
            // 
            categoriasToolStripMenuItem.Name = "categoriasToolStripMenuItem";
            categoriasToolStripMenuItem.Size = new Size(191, 30);
            categoriasToolStripMenuItem.Text = "Categorias";
            categoriasToolStripMenuItem.Click += categoriasToolStripMenuItem_Click;
            // 
            // periodosToolStripMenuItem
            // 
            periodosToolStripMenuItem.Name = "periodosToolStripMenuItem";
            periodosToolStripMenuItem.Size = new Size(191, 30);
            periodosToolStripMenuItem.Text = "Ciclo Escolar";
            periodosToolStripMenuItem.Click += periodosToolStripMenuItem_Click;
            // 
            // maToolStripMenuItem
            // 
            maToolStripMenuItem.Name = "maToolStripMenuItem";
            maToolStripMenuItem.Size = new Size(191, 30);
            maToolStripMenuItem.Text = "Instituciones";
            maToolStripMenuItem.Click += maToolStripMenuItem_Click;
            // 
            // cicloEscolarToolStripMenuItem
            // 
            cicloEscolarToolStripMenuItem.Name = "cicloEscolarToolStripMenuItem";
            cicloEscolarToolStripMenuItem.Size = new Size(191, 30);
            cicloEscolarToolStripMenuItem.Text = "Materias";
            cicloEscolarToolStripMenuItem.Click += cicloEscolarToolStripMenuItem_Click;
            // 
            // periodosToolStripMenuItem1
            // 
            periodosToolStripMenuItem1.Name = "periodosToolStripMenuItem1";
            periodosToolStripMenuItem1.Size = new Size(191, 30);
            periodosToolStripMenuItem1.Text = "Periodos";
            periodosToolStripMenuItem1.Click += periodosToolStripMenuItem1_Click;
            // 
            // consultasToolStripMenuItem
            // 
            consultasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { actividadesToolStripMenuItem, calificacionesToolStripMenuItem });
            consultasToolStripMenuItem.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            consultasToolStripMenuItem.Name = "consultasToolStripMenuItem";
            consultasToolStripMenuItem.Size = new Size(106, 29);
            consultasToolStripMenuItem.Text = "Consultas";
            // 
            // actividadesToolStripMenuItem
            // 
            actividadesToolStripMenuItem.Name = "actividadesToolStripMenuItem";
            actividadesToolStripMenuItem.Size = new Size(200, 30);
            actividadesToolStripMenuItem.Text = "Actividades";
            // 
            // calificacionesToolStripMenuItem
            // 
            calificacionesToolStripMenuItem.Name = "calificacionesToolStripMenuItem";
            calificacionesToolStripMenuItem.Size = new Size(200, 30);
            calificacionesToolStripMenuItem.Text = "Calificaciones";
            // 
            // reportesToolStripMenuItem
            // 
            reportesToolStripMenuItem.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            reportesToolStripMenuItem.Size = new Size(97, 29);
            reportesToolStripMenuItem.Text = "Reportes";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 708);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1318, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(39, 17);
            toolStripStatusLabel.Text = "Status";
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1318, 730);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menú";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private ToolStripMenuItem formulariosToolStripMenuItem;
        private ToolStripMenuItem institutosToolStripMenuItem;
        private ToolStripMenuItem categoriasToolStripMenuItem;
        private ToolStripMenuItem periodosToolStripMenuItem;
        private ToolStripMenuItem maToolStripMenuItem;
        private ToolStripMenuItem cicloEscolarToolStripMenuItem;
        private ToolStripMenuItem periodosToolStripMenuItem1;
        private ToolStripMenuItem consultasToolStripMenuItem;
        private ToolStripMenuItem actividadesToolStripMenuItem;
        private ToolStripMenuItem calificacionesToolStripMenuItem;
        private ToolStripMenuItem reportesToolStripMenuItem;
    }
}



