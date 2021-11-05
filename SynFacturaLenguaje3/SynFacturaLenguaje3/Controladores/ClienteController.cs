using SynFacturaLenguaje3.Modelos.DAO;
using SynFacturaLenguaje3.Modelos.Entidades;
using SynFacturaLenguaje3.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynFacturaLenguaje3.Controladores
{
    public class ClienteController
    {
        ClientesView vista = new ClientesView();
        ClienteDAO clienteDAO = new ClienteDAO();
        Cliente cliente = new Cliente();
        string operacion = ""; 

        public ClienteController (ClientesView view )
        {
            vista = view;
            vista.NuevoButton.Click += new EventHandler(Nuevo);
            vista.GuardarButton.Click += new EventHandler(Guardar); 
        }

        private void Guardar(object sender, EventArgs e)
        {
            if (vista.IdentidadMaskedTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.NombreTextBox, "Debes ingresar un número de identidad");
                vista.IdentidadMaskedTextBox.Focus();
                return;
            }
            if (vista.NombreTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.NombreTextBox, "Debes ingresar un nombre");
                vista.NombreTextBox.Focus();
                return;
            }

            if (vista.EmailTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.EmailTextBox, "Debes ingresar un email");
                vista.EmailTextBox.Focus();
                return;
            }

            if (vista.DireccionTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.DireccionTextBox, "Debes ingresar una direccion");
                vista.DireccionTextBox.Focus();
                return;
            }

            cliente.Identidad = vista.IdentidadMaskedTextBox.Text; 
            cliente.Nombre = vista.NombreTextBox.Text;
            cliente.Email = vista.EmailTextBox.Text;
            cliente.Direccion = vista.DireccionTextBox.Text;
            //cliente.Foto = vista.FotoPictureBox.Image;
            cliente.Id = Convert.ToInt32(vista.IdTextBox.Text);

            if (operacion == "Nuevo")
            {
                bool inserto = clienteDAO.InsertarNuevoCliente(cliente);

                if (inserto)
                {
                    ListarUsuarios();
                    MessageBox.Show("Cliente creado exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DeshabilitarControles();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("No se pudo crear el cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (operacion == "Modificar")
            {


                bool modifico = clienteDAO.ActualizarCliente(cliente);

                if (modifico)
                {
                    MessageBox.Show("Cliente modificado exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DeshabilitarControles();
                    LimpiarControles();
                    ListarUsuarios();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void Nuevo(object sender, EventArgs e)
        {
            HabilitarControles();
            operacion = "Nuevo"; 
        }

        private void HabilitarControles()
        {
            vista.IdentidadMaskedTextBox.Enabled = true;
            vista.NombreTextBox.Enabled = true;
            vista.EmailTextBox.Enabled = true;
            vista.DireccionTextBox.Enabled = true;

            vista.GuardarButton.Enabled = true;
            vista.CancelarButton.Enabled = true;
            vista.ImagenButton.Enabled = true; 

            vista.NuevoButton.Enabled = false;
            vista.ModificarButton.Enabled = false; 
        }

        private void DeshabilitarControles()
        {
            vista.IdentidadMaskedTextBox.Enabled = true;
            vista.NombreTextBox.Enabled = true;
            vista.EmailTextBox.Enabled = true;
            vista.DireccionTextBox.Enabled = true;

            vista.GuardarButton.Enabled = true;
            vista.CancelarButton.Enabled = true;
            vista.ImagenButton.Enabled = true;

            vista.NuevoButton.Enabled = false;
            vista.ModificarButton.Enabled = false;
        }

        private void LimpiarControles()
        {
            vista.IdentidadMaskedTextBox.Clear();
            vista.NombreTextBox.Clear();
            vista.EmailTextBox.Clear();
            vista.DireccionTextBox.Clear();
        }

        private void ListarUsuarios()
        {
            vista.ClientesDataGridView.DataSource = clienteDAO.GetClientes();
        }
    }
}
