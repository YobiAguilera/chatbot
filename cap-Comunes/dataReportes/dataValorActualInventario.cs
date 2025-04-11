using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes.dataReportes
{
    public class dataValorActualInventario
    {
        public string articulo { get; set; }
        public string existencias { get; set; }
        public decimal precioUnitario { get; set;  }
        public decimal subtotal { get; set; }
        public string sucursal { get; set; }
        public string sku { get; set; }

    }
}
