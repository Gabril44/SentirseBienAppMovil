namespace MauiApp1.Views;

public partial class MisPagos : ContentPage
{
    private Pago pagoSeleccionado; 
    public MisPagos()
	{
        pagoSeleccionado = new Pago();
		InitializeComponent();
	}
    private async Task AnimateButton(Button button)
    {
        // Escala el botón a un 90% de su tamaño original
        await button.ScaleTo(0.9, 100, Easing.CubicIn);
        // Vuelve el botón a su tamaño original
        await button.ScaleTo(1, 100, Easing.CubicOut);
    }
    private async void OnPagarButtonClicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            pagoSeleccionado = (Pago)button.CommandParameter;
            await AnimateButton(button);
        }
        await Navigation.PushAsync(new Views.RealizarPago(pagoSeleccionado));
    }


}