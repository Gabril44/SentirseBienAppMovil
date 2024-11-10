using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentirseBien
{
    internal class TurnoConsultas
    {
        private ConexionMysql conexionMysql;
        private ObservableCollection<Turno> turnos;

        public TurnoConsultas()
        {
            conexionMysql = new ConexionMysql();
            turnos = new ObservableCollection<Turno>();
        }

        public ObservableCollection<Turno> getTurno(string filtro)
        {
            ObservableCollection<Turno> turnos = new ObservableCollection<Turno>(); // Inicializa la lista
            string QUERY = "SELECT * FROM turnos WHERE nombre_usuario = @nombre_usuario";  // Filtra por el usuario logueado

            try
            {
                // Verifica si se ha proporcionado un filtro adicional (por ejemplo, por idturno, nombre_usuario, etc.)
                if (!string.IsNullOrEmpty(filtro)) // Si el filtro no está vacío o nulo
                {
                    QUERY += " AND (" +
                        "idturno LIKE @filtro OR " +
                        "nombre_usuario LIKE @filtro OR " +
                        "fecha LIKE @filtro OR " +
                        "servicio LIKE @filtro OR " +
                        "profesional LIKE @filtro)";
                }

                using (MySqlConnection connection = conexionMysql.GetConnection()) // Usar 'using' para manejar la conexión
                {
                    // Verificar si la conexión está cerrada antes de abrirla
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open(); // Abre la conexión solo si está cerrada
                    }

                    MySqlCommand comando = new MySqlCommand(QUERY, connection); // Asocia el comando a la conexión

                    // Agregar parámetros para evitar inyecciones SQL
                    comando.Parameters.AddWithValue("@nombre_usuario", MauiApp1.App.usuariologeado.nombre); // Filtro por usuario logueado
                    if (!string.IsNullOrEmpty(filtro))
                    {
                        comando.Parameters.AddWithValue("@filtro", "%" + filtro + "%"); // Agrega el filtro de búsqueda
                    }

                    using (MySqlDataReader reader = comando.ExecuteReader()) // Usar 'using' para manejar el reader
                    {
                        while (reader.Read())
                        {
                            Turno turno = new Turno();
                            turno.idturnos = reader.GetInt16("idturnos");
                            turno.nombre_usuario = reader.GetString("nombre_usuario");
                            turno.fecha = reader.GetString("fecha");
                            turno.servicio = reader.GetString("servicio");
                            turno.profesional = reader.GetString("profesional");
                            turnos.Add(turno);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine("Error al obtener los turnos: " + ex.Message);
            }

            return turnos;
        }


    }
}
