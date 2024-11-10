using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class DatabaseService
    {
        
            private string connectionString = "Server=boxb4cfvsxmkcnxhmkbk-mysql.services.clever-cloud.com;Database=boxb4cfvsxmkcnxhmkbk;User=u8qzi582zuyygfde;Password=dqOUuQMxD7eohFZHPCZL;";


        public async Task<bool> LoginAsync(string username, string password)
        {
                bool isAuthenticated = false;

                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        // Consulta SQL para verificar el usuario
                        string query = "SELECT COUNT(*) FROM usuarios WHERE user = @username AND password = @password";
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@username", username);
                            command.Parameters.AddWithValue("@password", password);

                            var result = (long)await command.ExecuteScalarAsync();
                            isAuthenticated = result > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores, por ejemplo, mostrar un mensaje de error
                    Console.WriteLine("Error de conexión: " + ex.Message);
                }

                return isAuthenticated;
        }

        public string getConnectionString() { return connectionString; }

        
    }
}
