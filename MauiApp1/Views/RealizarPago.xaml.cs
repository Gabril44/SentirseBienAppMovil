namespace MauiApp1.Views;

public partial class RealizarPago : ContentPage
{
    private decimal monto;
    private Pago pagoseleccionado;
    private readonly ConexionMysql conexionMysql;
    private PagoService pagoService;
    public RealizarPago(Pago pagoseleccionado)
	{
		InitializeComponent(); 
        this.pagoseleccionado = pagoseleccionado;
        this.monto = pagoseleccionado.monto*0.90m;
        TotalLabel.Text = $"Total : ${monto:N2}";
        conexionMysql = new ConexionMysql();
        pagoService = new PagoService();
    }



    private async void OnConfirmarPagoClicked(object sender, EventArgs e)
    {
        // Aquí puedes validar los campos y procesar el pago
        await DisplayAlert("Datos :","nro:"+pagoseleccionado.nropago+"nombre: "+pagoseleccionado.nombre_cliente+ "id_user: "+pagoseleccionado.id_usuario+ "Servicio: "+pagoseleccionado.servicio+"Monto :" +monto.ToString(), "OK");

        await pagoService.ReflejarUnPagoAsync(pagoseleccionado, "Débito");

        await Navigation.PushAsync(new Views.HomePage());
    }
}