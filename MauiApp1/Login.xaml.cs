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
        // Obt�n los valores de los campos de usuario y contrase�a
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        // Llama al m�todo LoginAsync para verificar las credenciales
        bool isAuthenticated = await databaseService.LoginAsync(username, password);

        if (isAuthenticated)
        {
            await DisplayAlert("�xito", "Inicio de sesi�n exitoso", "OK");
            // Navegar a HomePage despu�s del inicio de sesi�n
            await Navigation.PushAsync(new Views.HomePage());

        }
        else
        {
            await DisplayAlert("Error", "Usuario o contrase�a incorrectos", "OK");
        }
    }
}