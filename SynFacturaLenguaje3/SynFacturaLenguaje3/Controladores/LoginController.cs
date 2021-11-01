using SynFacturaLenguaje3.Modelos.DAO;
using SynFacturaLenguaje3.Modelos.Entidades;
using SynFacturaLenguaje3.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynFacturaLenguaje3.Controladores
{
    public class LoginController
    {
        LoginView Vista; 
        public LoginController(LoginView vista)
        {
            Vista = vista;
            Vista.BtnAceptar.Click += new EventHandler(ValidarUsuario); 
        }

        private void ValidarUsuario(object sender, EventArgs e)
        {
            bool esValido = false; 
            UsuarioDAO userDao = new UsuarioDAO();

            Usuario user = new Usuario();
            user.Email = Vista.TxtEmail.Text;
            user.Clave = EncriptarClave(Vista.TxtContraseña.Text);

            esValido = userDao.ValidarUsuario(user);

            if (esValido) {
                //MessageBox.Show("Usuario correcto");

                MenuView menu = new MenuView();
                Vista.Hide(); 
                menu.Show(); 


            }  
            else MessageBox.Show("Usuario incorrecto");
        }

        public static string EncriptarClave(string str)
        {
            string cadena = str + "MiClavePersonal";
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(cadena));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString(); 
        }
    }
}
