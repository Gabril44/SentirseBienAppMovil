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
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        Usuario usuario = await databaseService.LoginAsync(username, password);

        if (usuario != null)
        {
            App.usuariologeado = usuario;  // Establece el usuario logueado
            if (App.usuariologeado.rol == 1) 
            {
                await DisplayAlert("Solo para clientes", "Hola, esta app es solo para clientes!", "OK");
            }
            else 
            {
                await DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");
                await Navigation.PushAsync(new Views.HomePage());
            }

        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
        }
    }


}