using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataAbonosClientes
    {
        public string folio { get; set; }
        public string fecha { get; set; }
        public string monto { get; set; }
        public string ruta { get; set; }
        public double serviciosExtra { get; set; }
        public double costoTotal { get; set; }
        public double costoPlan { get; set; }
        public double saldoPendiente { get; set; }
        public string tipoRecibo { get; set; }
        public double abonado { get; set; }
    }
}
