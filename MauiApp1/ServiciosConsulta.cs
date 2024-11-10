using MySql.Data.MySqlClient;
using SentirseBien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    internal class ServiciosConsulta
    {
        private Conexion conexion;
        private ConexionMysql conexionMysql;
        private DatabaseService _db;
        private List<Servicio> servicios;

        public ServiciosConsulta() 
        {
            _db = new DatabaseService();
            servicios = new List<Servicio>();
            conexion = new ConexionMysql();
            conexionMysql = new ConexionMysql();
        }
        public List<Servicio> getServicio(string filtro)
        {
            string QUERY = "SELECT * FROM servicios";
            MySqlDataReader reader = null;
            try
            {
                if (filtro != "")
                {
                    QUERY += " WHERE " +
                        "nombre LIKE '%" + filtro + "%' OR " +
                        "precio LIKE '%" + filtro + "%' OR " +
                        "numServicio LIKE '%" + filtro + "%';";
                }

                MySqlCommand comando = new MySqlCommand(QUERY);
                comando.Connection = conexionMysql.GetConnection();
                reader = comando.ExecuteReader();

                Servicio servicio = null;
                while (reader.Read())
                {
                    servicio = new Servicio();
                    servicio.nombre = reader.GetString("nombre");
                    servicio.precio = reader.GetInt32("precio");
                    servicio.numServicio = reader.GetInt32("numServicio");
                    servicios.Add(servicio);
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return servicios;
        }
    }
}
