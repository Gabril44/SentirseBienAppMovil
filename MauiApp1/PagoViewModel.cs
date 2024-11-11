using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1
{
    public class PagoViewModel
    {
        public ObservableCollection<Turno> ListaPagos { get; set; } = new ObservableCollection<Turno>();
        public ICommand PagarCommand { get; }

        public PagoViewModel()
        {
            PagarCommand = new Command<Pago>(Pagar);
            CargarPago();
        }
        private void Pagar(Pago pago)
        {
            Console.WriteLine($"Pagar turno de {pago.nombre_cliente} por el servicio {pago.servicio}");
        }

        private void CargarPago(string filtro = "")
        {
            TurnoConsultas turnoConsultas = new TurnoConsultas();
            var turnos = turnoConsultas.getTurno(filtro);

            // Limpia y carga los turnos en la colección observable
            ListaPagos.Clear();
            foreach (var turno in turnos)
            {
                ListaPagos.Add(turno);
            }
        }
    }
}
