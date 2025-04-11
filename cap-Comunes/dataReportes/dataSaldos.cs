using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataSaldos
    {
        public string periodo { get; set; }
        public string concepto { get; set; }
        public double ingreso { get; set; }
        public double egreso { get; set; }
        public double saldo { get; set; }
        public double acumulado { get; set; }
    }
}
