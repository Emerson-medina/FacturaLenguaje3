﻿using SynFacturaLenguaje3.Modelos.DAO;
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
    public class UsuarioController
    {
        UsuariosView vista;
        string operacion = String.Empty; 
        public UsuarioController( UsuariosView view)
        {
            vista = view;
            vista.NuevoButton.Click += new EventHandler(Nuevo);
            vista.GuardarButton.Click += new EventHandler(Guardar); 
        }


        private void Nuevo(object senderm, EventArgs e)
        {
            HabilitarControles();
            operacion = "Nuevo";  
        }

        private void Guardar(object senderm, EventArgs e)
        {
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

            if (vista.ClaveTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.ClaveTextBox, "Debes ingresar una clave");
                vista.ClaveTextBox.Focus();
                return;
            }

            UsuarioDAO userDAO = new UsuarioDAO();
            Usuario user = new Usuario();

            user.Nombre = vista.NombreTextBox.Text;
            user.Email = vista.EmailTextBox.Text;
            user.Clave = vista.ClaveTextBox.Text;
            user.EsAdministrador = vista.EsAdministradorCheckBox.Checked;

            bool inserto = userDAO.InsertarNuevoUsuario(user); 

            if (inserto)
            {
                MessageBox.Show("Usuario creado exitosamente" ,"Atención" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
                DesabilitarControles();
                LimpiarControles(); 
            } else
            {
                MessageBox.Show("No se pudo crear el usuario", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void LimpiarControles()
        {
            vista.IdTextBox.Text = "";
            vista.NombreTextBox.Text = "";
            vista.EmailTextBox.Text = "";
            vista.ClaveTextBox.Text = "";
            vista.EsAdministradorCheckBox.Enabled = false; 
        }

        private void HabilitarControles()
        {
            vista.IdTextBox.Enabled = true;
            vista.NombreTextBox.Enabled = true;
            vista.EmailTextBox.Enabled = true;
            vista.ClaveTextBox.Enabled = true;
            vista.EsAdministradorCheckBox.Enabled = true;

            vista.GuardarButton.Enabled = true;
            vista.CancelarButton.Enabled = true;
            vista.ModificarButton.Enabled = false; 
            vista.NuevoButton.Enabled = false;
        }

        private void DesabilitarControles()
        {
            vista.IdTextBox.Enabled = false;
            vista.NombreTextBox.Enabled = false;
            vista.EmailTextBox.Enabled = false;
            vista.ClaveTextBox.Enabled = false;
            vista.EsAdministradorCheckBox.Enabled = false;

            vista.GuardarButton.Enabled = false;
            vista.CancelarButton.Enabled = false;
            vista.ModificarButton.Enabled = true;
            vista.NuevoButton.Enabled = true;
        }

    }
}
