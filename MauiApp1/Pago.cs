using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class Pago
    {
        public int nropago { get; set; }
        public decimal monto { get; set; }
        public string nombre_cliente { get; set; }
        public string fecha { get; set; }
        public string medio_de_pago { get; set; }
        public string estado {  get; set; }
        public int id_usuario { get; set; }
        public string fechalimite { get; set; }
        public string servicio { get; set; }
        public string profesional { get; set; }
        
    }
}
