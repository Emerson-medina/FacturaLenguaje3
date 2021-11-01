using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SynFacturaLenguaje3.Vistas
{
    public partial class MenuView : Syncfusion.Windows.Forms.Office2010Form
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void toolStripTabItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripEx1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void UsuariosToolStripButton_Click(object sender, EventArgs e)
        {
            UsuariosView vista = new UsuariosView();
            vista.Show();  
        }
    }
}
