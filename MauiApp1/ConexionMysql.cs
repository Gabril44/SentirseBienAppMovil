using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentirseBien
{
    public class ConexionMysql : Conexion
    {
        private MySqlConnection connection;
        private string cadenaConexion;
        public ConexionMysql() 
        {
            cadenaConexion = "Database=" + database +
                "; DataSource=" + server +
                "; User Id ="+ username +
                "; Password="+ password;
            connection = new MySqlConnection(cadenaConexion);
        }

        public MySqlConnection GetConnection() 
        {
            try 
            {
                if (connection.State != System.Data.ConnectionState.Open) 
                {
                    connection.Open();
                }
            } 
            catch (Exception ex) 
            {
                //MessageBox.Show(ex.ToString());
            }
            return connection;
        }

        // Método para cerrar la conexión
        public void CerrarConexion()
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al cerrar la conexión: " + ex.Message);
            }
        }
    }
}
