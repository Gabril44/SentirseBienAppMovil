using MauiApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    internal class RealizarPagoViewModel : BaseViewModel
    {
        public decimal totalConDescuento { get; set; }
        private readonly PagoService _pagoService;
        public ObservableCollection<Pago> Pagos => _pagoService.Pagos;
        public RealizarPagoViewModel(decimal montoOrigial, PagoService pagoService) 
        {
            if (montoOrigial > 0)
            {
                this.totalConDescuento = montoOrigial * 0.90m;
            }
            _pagoService = pagoService;

            string mediodepago = "Débito";
            _pagoService.ReflejarPagoAsync(_pagoService.Pagos, mediodepago);
        }

        
    }
}
