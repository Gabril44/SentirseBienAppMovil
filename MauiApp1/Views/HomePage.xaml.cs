namespace MauiApp1.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void OnPedirTurnoButtonClicked(object sender, EventArgs e) 
	{
        DisplayAlert("Hiciste click!", "Vamos a la pagina para pedir turno", "OK");
        await Navigation.PushAsync(new Views.PedirTurno());
    }

    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }

    private async void OnMisTurnosClicked(object sender, EventArgs e) 
    {
        await Navigation.PushAsync(new Views.MisTurnos());
    }

    private async void OnPagarClicked(object sender, EventArgs e) 
    {
        await Navigation.PushAsync(new Views.MisPagos());
    }
}