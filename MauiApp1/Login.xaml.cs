namespace MauiApp1;

public partial class Login : ContentPage
{
    private DatabaseService databaseService;

    public Login()
    {
        InitializeComponent();
        databaseService = new DatabaseService();
    }


    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // Obtén los valores de los campos de usuario y contraseña
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        // Llama al método LoginAsync para verificar las credenciales
        bool isAuthenticated = await databaseService.LoginAsync(username, password);

        if (isAuthenticated)
        {
            await DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");
            // Navegar a HomePage después del inicio de sesión
            await Navigation.PushAsync(new Views.HomePage());

        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
        }
    }
}