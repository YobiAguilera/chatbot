using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataReporteCompras
    {
        public string noCompra { get; set; }
        public string fechaCompra { get; set; }
        public string proveedor { get; set; }
        public string usuario { get; set; }
        public double subtotal { get; set; }
        public double iva { get; set; }
        public double total { get; set; }
        public string observaciones { get; set; }
        public string estatus { get; set; }
    }
}
