using SynFacturaLenguaje3.Controladores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynFacturaLenguaje3.Vistas
{
    public partial class UsuariosView : Form
    {
        public UsuariosView()
        {
            InitializeComponent();

            UsuarioController controlador = new UsuarioController(this); 

        }
    }
}
