using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1
{
    public class TurnoViewModel
    {
        public ObservableCollection<Turno> ListaTurnos { get; set; } = new ObservableCollection<Turno>();
        public ICommand PagarCommand { get; }

        public TurnoViewModel()
        {
            PagarCommand = new Command<Turno>(Pagar);
            CargarTurnos();
        }
        private void Pagar(Turno turno) 
        {
            Console.WriteLine($"Pagar turno de {turno.nombre_usuario} por el servicio {turno.servicio}");
        }

        private void CargarTurnos(string filtro = "")
        {
            TurnoConsultas turnoConsultas = new TurnoConsultas();
            var turnos = turnoConsultas.getTurno(filtro);

            // Limpia y carga los turnos en la colección observable
            ListaTurnos.Clear();
            foreach (var turno in turnos)
            {
                ListaTurnos.Add(turno);
            }
        }

        private async void PagarTurno(Turno turno)
        {
            // Aquí implementas la lógica de pago
            await Application.Current.MainPage.DisplayAlert("Pago", $"Pagando el turno para el servicio {turno.servicio}", "OK");

            // También puedes actualizar el estado del turno en la base de datos y refrescar la lista si es necesario
        }


    }
}
