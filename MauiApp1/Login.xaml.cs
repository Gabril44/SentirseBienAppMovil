namespace MauiApp1;

public partial class Login : ContentPage
{
    private DatabaseService databaseService;

    public Login()
    {
        InitializeComponent();
        databaseService = new DatabaseService();
    }

    private async Task AnimateButton(Button button)
    {
        // Escala el bot�n a un 90% de su tama�o original
        await button.ScaleTo(0.9, 100, Easing.CubicIn);
        // Vuelve el bot�n a su tama�o original
        await button.ScaleTo(1, 100, Easing.CubicOut);
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            await AnimateButton(button);
        }
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
                await DisplayAlert("�xito", "Inicio de sesi�n exitoso", "OK");
                await Navigation.PushAsync(new Views.HomePage());
            }

        }
        else
        {
            await DisplayAlert("Error", "Usuario o contrase�a incorrectos", "OK");
        }
    }


}