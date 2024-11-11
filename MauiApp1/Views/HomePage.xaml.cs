namespace MauiApp1.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async Task AnimateButton(Button button)
    {
        // Escala el botón a un 90% de su tamaño original
        await button.ScaleTo(0.9, 100, Easing.CubicIn);
        // Vuelve el botón a su tamaño original
        await button.ScaleTo(1, 100, Easing.CubicOut);
    }
    private async void OnPedirTurnoButtonClicked(object sender, EventArgs e) 
	{
        Button button = sender as Button;
        if (button != null)
        {
            await AnimateButton(button);
        }
        await Navigation.PushAsync(new Views.PedirTurno());
    }

    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            await AnimateButton(button);
        }
        await Navigation.PushAsync(new Login());
    }

    private async void OnMisTurnosClicked(object sender, EventArgs e) 
    {
        Button button = sender as Button;
        if (button != null)
        {
            await AnimateButton(button);
        }
        await Navigation.PushAsync(new Views.MisTurnos());
    }

    private async void OnPagarClicked(object sender, EventArgs e) 
    {
        Button button = sender as Button;
        if (button != null)
        {
            await AnimateButton(button);
        }
        await Navigation.PushAsync(new Views.MisPagos());
    }
}