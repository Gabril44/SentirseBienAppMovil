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
        // Escala el bot�n a un 90% de su tama�o original
        await button.ScaleTo(0.9, 100, Easing.CubicIn);
        // Vuelve el bot�n a su tama�o original
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