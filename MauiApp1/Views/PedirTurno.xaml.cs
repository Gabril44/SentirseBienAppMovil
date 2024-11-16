using MySql.Data.MySqlClient;
using System.Data;

namespace MauiApp1.Views;

public partial class PedirTurno : ContentPage
{
    private Servicio servicio1;
    private Turno turnoCreado;
    private Pago pagoCreado;
    private List<Servicio> servicios;
    private List<string> nombres_servicios;
    public PedirTurno()
    {
        InitializeComponent();
        LoadHorarios();
        LoadServicios();
    }

    private async void OnPedirTurnoButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Click!", "Usted clickeó aceptar", "OK");
        crearTurno();
        await InsertarTurnoAsync(turnoCreado);
        crearPago();
        await InsertarPagoPendienteAsync(pagoCreado);
        await DisplayAlert("Listo", "Volvamos al inicio", "OK");
        await Navigation.PushAsync(new Views.HomePage());
    }

    private void crearTurno() 
    {
        turnoCreado = new Turno();
        turnoCreado.fecha =  selectedDateLabel.Text + " " + selectedHourLabel.Text;
        turnoCreado.nombre_usuario = App.usuariologeado.user;
        turnoCreado.profesional = selectedProfesionalLabel.Text;
        turnoCreado.servicio = selectedServiceLabel.Text;
    }

    private void crearPago() 
    {
        pagoCreado = new Pago();
        pagoCreado.profesional = selectedProfesionalLabel.Text;
        pagoCreado.nombre_cliente = App.usuariologeado.nombre;
        pagoCreado.estado = "pendiente";
        pagoCreado.medio_de_pago = "pendiente";
        pagoCreado.fecha = turnoCreado.fecha;
        for (int i = 0; i < servicios.Count; i++) 
        {
            if (servicios[i].nombre == turnoCreado.servicio) 
            {
                pagoCreado.servicio = servicios[i].nombre;
                pagoCreado.monto = servicios[i].precio;
            }
        }
        pagoCreado.id_usuario = App.usuariologeado.idusuario;
        // Parsear la fecha de turnoCreado a DateTime
        DateTime fechaTurno = DateTime.Parse(turnoCreado.fecha);

        // Agregarle 2 días
        DateTime fechaConDosDias = fechaTurno.AddDays(2);

        // Convertir de nuevo a string en el formato deseado
        pagoCreado.fechalimite = fechaConDosDias.ToString("dd/MM/yyyy"); // ajustar el formato 
    }

    public async Task InsertarTurnoAsync(Turno turno)
    {
        ConexionMysql conexionMysql = new ConexionMysql();
        using (MySqlConnection connection = conexionMysql.GetConnection())  // Obtiene nueva conexión
        {
            //await connection.OpenAsync(); // Asegúrate de que la conexión está abierta

            string QUERY = "INSERT INTO turnos (nombre_usuario, fecha, servicio, profesional) " +
                           "VALUES (@nombre_usuario, @fecha, @servicio, @profesional)";

            using (MySqlCommand command = new MySqlCommand(QUERY, connection))
            {
                // Asigna los valores de los parámetros
                command.Parameters.AddWithValue("@nombre_usuario", turno.nombre_usuario);
                command.Parameters.AddWithValue("@fecha", turno.fecha);
                command.Parameters.AddWithValue("@servicio", turno.servicio);
                command.Parameters.AddWithValue("@profesional", turno.profesional);

                // Ejecuta la consulta de inserción
                await command.ExecuteNonQueryAsync();
            }
            // La conexión se cierra automáticamente aquí al salir del bloque using
        }
    }


    public async Task InsertarPagoPendienteAsync(Pago pago)
    {
        ConexionMysql conexionMysql = new ConexionMysql();
        using (MySqlConnection connection = conexionMysql.GetConnection())  // Obtiene nueva conexión
        {
            //await connection.OpenAsync(); // Asegúrate de que la conexión está abierta

            string QUERY = "INSERT INTO pagos (monto, nombrecliente, fecha, mediodepago, estado, id_usuario, fechalimite, servicio, profesional) " +
                           "VALUES (@monto, @nombre_cliente, @fecha, @medio_de_pago, @estado, @id_usuario, @fechalimite, @servicio, @profesional)";

            using (MySqlCommand command = new MySqlCommand(QUERY, connection))
            {
                // Asigna los valores de los parámetros
                command.Parameters.AddWithValue("@monto", pago.monto);
                command.Parameters.AddWithValue("@nombre_cliente", pago.medio_de_pago);
                command.Parameters.AddWithValue("@fecha", pago.fecha);
                command.Parameters.AddWithValue("@medio_de_pago", pago.medio_de_pago);
                command.Parameters.AddWithValue("@estado", pago.estado);
                command.Parameters.AddWithValue("@id_usuario", pago.id_usuario);
                command.Parameters.AddWithValue("@fechalimite", pago.fechalimite);
                command.Parameters.AddWithValue("@servicio", pago.servicio);
                command.Parameters.AddWithValue("@profesional", pago.profesional);

                // Ejecuta la consulta de inserción
                await command.ExecuteNonQueryAsync();
            }
            // La conexión se cierra automáticamente aquí al salir del bloque using
        }
    }


    private void LoadServicios()
    {
        servicios = new List<Servicio>();
        ConexionMysql conexionMysql = new ConexionMysql();
        // Lista para los servicios
        nombres_servicios = new List<string>();
        string QUERY = "SELECT nombre, precio FROM servicios";

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
                        Servicio nuevoServicio = new Servicio();
                        nuevoServicio.nombre = reader.GetString("nombre");
                        nuevoServicio.precio = reader.GetInt32("precio");
                        nombres_servicios.Add(nuevoServicio.nombre);
                        servicios.Add(nuevoServicio);
                    }
                }
                myPickerServicio.ItemsSource = nombres_servicios;

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
        selectedDateLabel.Text = e.NewDate.ToString("dd/MM/yyyy");
    }

    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        // Verificar que el Picker tiene un elemento seleccionado
        if (myPicker.SelectedIndex != -1)
        {
            // Obtener el texto de la opción seleccionada
            string selectedHour = myPicker.Items[myPicker.SelectedIndex];
            // Asignar el texto al Label para mostrar el horario seleccionado
            selectedHourLabel.Text = selectedHour;
        }
    }

    private void OnServicePickerSelectedIndexChanged(Object sender, EventArgs e) 
    {
        if (myPickerServicio.SelectedIndex != -1) 
        {
            string selectedService = myPickerServicio.Items[myPickerServicio.SelectedIndex];
            // Asignar el texto al Label para mostrar el servicio seleccionado
            selectedServiceLabel.Text = selectedService;
        }
    }

    private void OnProfesionalPickerSelectedIndexChanged(Object sender, EventArgs e) 
    {
        string selectedProfesional = myPickerProfesional.Items[myPickerProfesional.SelectedIndex];
        selectedProfesionalLabel.Text = selectedProfesional;
    }


}