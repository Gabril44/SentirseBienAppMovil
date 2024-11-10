using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class DatabaseService
    {
        
            private string connectionString = "Server=boxb4cfvsxmkcnxhmkbk-mysql.services.clever-cloud.com;Database=boxb4cfvsxmkcnxhmkbk;User=u8qzi582zuyygfde;Password=dqOUuQMxD7eohFZHPCZL;";


        public async Task<Usuario> LoginAsync(string username, string password)
        {
            Usuario usuario = null; // Inicializamos la variable como null

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Consulta SQL para verificar el usuario y obtener su información
                    string query = "SELECT idusuario, nombre, correo, rol FROM usuarios WHERE user = @username AND password = @password";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync()) // Si se encuentra el usuario
                            {
                                usuario = new Usuario();  // Creamos el objeto Usuario
                                usuario.idusuario = reader.GetInt32("idusuario");
                                usuario.nombre = reader.GetString("nombre");
                                usuario.correo = reader.GetString("correo");
                                usuario.rol = reader.GetInt32("rol");
                                usuario.user = username;  // Usamos el username como identificador
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores, por ejemplo, mostrar un mensaje de error
                Console.WriteLine("Error de conexión: " + ex.Message);
            }

            return usuario;  // Retorna el objeto Usuario o null si no se encontró
        }



        public string getConnectionString() { return connectionString; }

        
    }
}
