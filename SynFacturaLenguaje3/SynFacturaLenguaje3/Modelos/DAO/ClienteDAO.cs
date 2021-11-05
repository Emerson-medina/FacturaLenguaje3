using SynFacturaLenguaje3.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynFacturaLenguaje3.Modelos.DAO
{
    public class ClienteDAO:Conexion
    {
        SqlCommand comando = new SqlCommand();

        public bool ValidarCliente(Cliente cliente)
        {
            bool valido = false;

            try
            {
                StringBuilder consulta = new StringBuilder();
                consulta.Append("SELECT 1 FROM CLIENTE WHERE IDENTIDAD = @Identidad AND EMAIL = @Email;");

                comando.Connection = MiConexion;

                MiConexion.Open();

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta.ToString();
                comando.Parameters.Add("@Identidad", SqlDbType.NVarChar, 50).Value = cliente.Identidad;
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = cliente.Email;

                valido = Convert.ToBoolean(comando.ExecuteScalar());
                MiConexion.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("A ocurrido un problema al intentar conectar con la base de datos");
            }

            return valido;

        }

        public bool InsertarNuevoCliente(Cliente cliente)
        {
            bool inserto = false; 
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = MiConexion;

                StringBuilder consulta = new StringBuilder();
                consulta.Append("INSERT INTO USUARIO (IDENTIDAD,NOMBRE, EMAIL, DIRECCION, FOTO)");
                consulta.Append(" VALUES (@Identidad,@Nombre, @Email, @Direcccion, @Foto)");


                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta.ToString();
                comando.Parameters.AddWithValue("@Identidad", cliente.Nombre);
                comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                comando.Parameters.AddWithValue("@Email", cliente.Email);
                comando.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                comando.Parameters.AddWithValue("@Foto", cliente.Foto);

                comando.ExecuteNonQuery();
                MiConexion.Close();
                inserto = true;

            }
            catch (Exception)
            {
                inserto = false;
            }

            return inserto; 
        }

        public DataTable GetClientes()
        {
            DataTable tableClientes = new DataTable();

            try
            {
                StringBuilder consulta = new StringBuilder();
                consulta.Append("SELECT * FROM CLIENTE;");

                comando.Connection = MiConexion;

                MiConexion.Open();

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta.ToString();
                SqlDataReader dataReader = comando.ExecuteReader();
                tableClientes.Load(dataReader);
                MiConexion.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("A ocurrido un problema al intentar cargar los usuarios");
            }

            return tableClientes;

        }

        public bool ActualizarCliente(Cliente cliente)
        {
            bool modifico = false;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = MiConexion;

                StringBuilder consulta = new StringBuilder();
                consulta.Append("UPDATE CLIENTE ");
                consulta.Append("SET IDENTIDAD = @Identidad, NOMBRE = @Nombre, EMAIL = @Email, DIRECCION = @Direccion, FOTO = @Foto ");
                consulta.Append("WHERE ID = @Id;");

                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta.ToString();

                comando.Parameters.AddWithValue("@Identidad", cliente.Identidad);
                comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                comando.Parameters.AddWithValue("@Email", cliente.Email);
                comando.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                comando.Parameters.AddWithValue("@Foto", cliente.Foto);
                comando.Parameters.AddWithValue("@Id", cliente.Id);

                comando.ExecuteNonQuery();
                MiConexion.Close();
                modifico = true;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                modifico = false;
            }
            return modifico;
        }

        public bool EliminarUsuario(int id)
        {
            bool eliminio = false;
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = MiConexion;

                StringBuilder consulta = new StringBuilder();
                consulta.Append("DELETE FROM CLIENTE ");
                consulta.Append("WHERE ID = @Id;");

                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta.ToString();

                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
                MiConexion.Close();
                eliminio = true;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                eliminio = false;
            }
            return eliminio;
        }
    }
}
