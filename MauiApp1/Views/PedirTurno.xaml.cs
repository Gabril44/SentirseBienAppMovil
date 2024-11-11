using MySql.Data.MySqlClient;

namespace MauiApp1.Views;

public partial class PedirTurno : ContentPage
{
    private ConexionMysql conexionMysql;
    private Servicio servicio1;
    public PedirTurno()
    {
        InitializeComponent();
        conexionMysql = new ConexionMysql();
        LoadHorarios();
        LoadServicios();
    }

    private async void OnPedirTurnoButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Click!", "Usted clickeó aceptar", "OK");
    }


    private void LoadServicios()
    {
        // Lista para los servicios
        var servicios = new List<string>();
        string QUERY = "SELECT nombre FROM servicios";

        // Lista para los profesionales
        var profesionales = new List<string>();
        string QUERYPRO = "SELECT nombre FROM usuarios WHERE rol = 1";

        // Usamos una única conexión para ambas consultas
        using (MySqlConnection connection = conexionMysql.GetConnection())
        {
            try
            {
                // Aseguramos que la conexión está abierta
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                // Ejecutar la primera consulta para obtener los servicios
                MySqlCommand command = new MySqlCommand(QUERY, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        servicios.Add(reader.GetString("nombre"));
                    }
                }
                myPickerServicio.ItemsSource = servicios;

                // Ejecutar la segunda consulta para obtener los profesionales
                command.CommandText = QUERYPRO; // Cambiar la consulta en el mismo objeto command
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        profesionales.Add(reader.GetString("nombre"));
                    }
                }
                myPickerProfesional.ItemsSource = profesionales;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error: ", ex.Message, "OK");
            }
        }
    }



    private void LoadHorarios()
    {
        // Crear un array para almacenar los horarios
        var horarios = new System.Collections.Generic.List<string>();

        // Hora de inicio (9 AM) y hora de fin (8 PM)
        DateTime startTime = DateTime.Today.AddHours(9);  // 9 AM hoy
        DateTime endTime = DateTime.Today.AddHours(20);   // 8 PM hoy

        // Generar los horarios en intervalos de 30 minutos
        while (startTime <= endTime)
        {
            horarios.Add(startTime.ToString("HH:mm"));
            startTime = startTime.AddMinutes(30);  // Incrementar 30 minutos
        }

        // Asignar la lista generada al Picker
        myPicker.ItemsSource = horarios;
    }

    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        selectedDateLabel.Text = e.NewDate.ToString("D");
    }

}