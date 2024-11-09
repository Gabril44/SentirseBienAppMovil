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
        Navigation.PushAsync(new Views.PedirTurno());
    }
}