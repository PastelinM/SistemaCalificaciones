using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistemaCalificaciones.UI.Views
{
    public partial class FormPrincipal : Form
    {
        private int childFormNumber = 0;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }



        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }


        private void institutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Crear una instancia (un objeto) de la ventana que quieres abrir
            FormAlumnos ventanaAlumnos = new FormAlumnos();

            // 2. Mostrar la ventana en modo "Diálogo"
            ventanaAlumnos.ShowDialog();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Crear una instancia (un objeto) de la ventana que quieres abrir
            FormCategorias ventanaCategorias = new FormCategorias();

            // 2. Mostrar la ventana en modo "Diálogo"
            ventanaCategorias.ShowDialog();
        }

        private void periodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Crear una instancia (un objeto) de la ventana que quieres abrir
            FormCiclos ventanaCescolar = new FormCiclos();

            // 2. Mostrar la ventana en modo "Diálogo"
            ventanaCescolar.ShowDialog();
        }

        private void maToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Crear una instancia (un objeto) de la ventana que quieres abrir
            FormInstitutos ventanaInstituciones = new FormInstitutos();

            // 2. Mostrar la ventana en modo "Diálogo"
            ventanaInstituciones.ShowDialog();
        }

        private void cicloEscolarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Crear una instancia (un objeto) de la ventana que quieres abrir
            FormMaterias ventanaMaterias = new FormMaterias();

            // 2. Mostrar la ventana en modo "Diálogo"
            ventanaMaterias.ShowDialog();

        }

        private void periodosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // 1. Crear una instancia (un objeto) de la ventana que quieres abrir
            FormPeriodos ventanaPeriodos = new FormPeriodos();

            // 2. Mostrar la ventana en modo "Diálogo"
            ventanaPeriodos.ShowDialog();

        }
    }
}
