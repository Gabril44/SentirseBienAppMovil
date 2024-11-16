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
        public ObservableCollection<Pago> ListaPagos { get; set; } = new ObservableCollection<Pago>();
        public ICommand PagarCommand { get; }

        public Pago pagoseleccionado;

        public PagoViewModel()
        {
            ListaPagos = new ObservableCollection<Pago>();
            PagarCommand = new Command<Pago>(Pagar);
            CargarPago();
        }
        
        private void Pagar(Pago pago)
        {
            Console.WriteLine($"Pagar turno de {pago.nombre_cliente} por el servicio {pago.servicio}");
        }

        private void CargarPago(string filtro = "")
        {
            PagosConsulta pagosConsultas = new PagosConsulta();
            var pagos = pagosConsultas.getPago(filtro);

            // Limpia y carga los turnos en la colección observable
            ListaPagos.Clear();
            foreach (var pago in pagos)
            {
                ListaPagos.Add(pago);
            }
        }
    }
}
