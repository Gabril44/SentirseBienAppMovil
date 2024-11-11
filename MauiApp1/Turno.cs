using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class Turno
    {
        public int idturnos { get; set; }
        public string nombre_usuario {  get; set; }
        public string fecha {  get; set; }
        public string servicio { get; set; }
        public string profesional { get; set; }
        public bool TienePagoPendiente { get; set; }


    }
}
