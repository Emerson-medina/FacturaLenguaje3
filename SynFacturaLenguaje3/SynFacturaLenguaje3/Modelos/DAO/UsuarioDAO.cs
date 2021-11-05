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
    public class UsuarioDAO : Conexion
    {
        SqlCommand comando = new SqlCommand(); 

        public bool ValidarUsuario (Usuario user)
        {
            bool valido = false;


            try
            {
                StringBuilder consulta = new StringBuilder();
                consulta.Append("SELECT 1 FROM USUARIO WHERE EMAIL = @Email AND CLAVE = @Clave;");

                comando.Connection = MiConexion;

                MiConexion.Open();

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta.ToString();
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;
                comando.Parameters.Add("@Clave", SqlDbType.NVarChar, 100).Value = user.Clave;
                
                valido = Convert.ToBoolean(comando.ExecuteScalar());
                MiConexion.Close(); 

            }
            catch (Exception)
            {
                MessageBox.Show("A ocurrido un problema al intentar conectar con la base de datos"); 
            }

            return valido; 

        }

        public bool InsertarNuevoUsuario(Usuario user)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = MiConexion;

                StringBuilder consulta = new StringBuilder();
                consulta.Append("INSERT INTO USUARIO (NOMBRE, EMAIL, CLAVE, ESADMINISTRADOR)");
                consulta.Append(" VALUES (@Nombre, @Email, @Clave, @EsAdministrador)");


                MiConexion.Open(); 
                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta.ToString();
                comando.Parameters.AddWithValue("@Nombre", user.Nombre);
                comando.Parameters.AddWithValue("@Email", user.Email);
                comando.Parameters.AddWithValue("@Clave", EncriptarClave(user.Clave));
                comando.Parameters.AddWithValue("@EsAdministrador", user.EsAdministrador);

                comando.ExecuteNonQuery();
                MiConexion.Close();
                return true;

            }
            catch (Exception)
            {
                return false; 
            }

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

        public DataTable GetUsuarios()
        {
            DataTable tableUsuarios = new DataTable(); 

            try
            {
                StringBuilder consulta = new StringBuilder();
                consulta.Append("SELECT * FROM USUARIO;");

                comando.Connection = MiConexion;

                MiConexion.Open();

                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta.ToString();
                SqlDataReader dataReader =  comando.ExecuteReader();
                tableUsuarios.Load(dataReader);
                MiConexion.Close(); 

            }
            catch (Exception)
            {
                MessageBox.Show("A ocurrido un problema al intentar cargar los usuarios");
            }

            return tableUsuarios; 

        }

        public bool ActualizarUsuario(Usuario user)
        {
            bool modifico = false; 
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = MiConexion;

                StringBuilder consulta = new StringBuilder();
                consulta.Append("UPDATE USUARIO ");
                consulta.Append("SET NOMBRE = @Nombre, EMAIL = @Email, CLAVE = @Clave, ESADMINISTRADOR = @EsAdministrador ");
                consulta.Append("WHERE ID = @Id;"); 

                MiConexion.Open();
                comando.CommandType = CommandType.Text;
                comando.CommandText = consulta.ToString();
                
                comando.Parameters.AddWithValue("@Nombre", user.Nombre);
                comando.Parameters.AddWithValue("@Email", user.Email);
                comando.Parameters.AddWithValue("@Clave", EncriptarClave(user.Clave));
                comando.Parameters.AddWithValue("@EsAdministrador", user.EsAdministrador);
                comando.Parameters.AddWithValue("@Id", user.Id);

                comando.ExecuteNonQuery();
                MiConexion.Close();
                modifico =  true;

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
                consulta.Append("DELETE FROM USUARIO ");
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
