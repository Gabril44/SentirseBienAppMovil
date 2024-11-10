using SentirseBien;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class TurnoViewModel
    {
        public ObservableCollection<Turno> ListaTurnos { get; set; } = new ObservableCollection<Turno>();

        public TurnoViewModel()
        {
            CargarTurnos();
        }

        private void CargarTurnos(string filtro = "")
        {
            TurnoConsultas turnoConsultas = new TurnoConsultas();
            var turnos = turnoConsultas.getTurno(filtro);

            // Limpiar y cargar la ObservableCollection
            ListaTurnos.Clear();
            foreach (var turno in turnos)
            {
                ListaTurnos.Add(turno);
            }
        }

    }
}
