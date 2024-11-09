namespace MauiApp1.Views;

public partial class PedirTurno : ContentPage
{
	public PedirTurno()
	{
		InitializeComponent();
        myPicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
    }
    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (myPicker.SelectedIndex != -1)
        {
            // Muestra la opción seleccionada en el Label
            selectedOptionLabel.Text = $"Seleccionaste: {myPicker.SelectedItem}";
        }
    }
}