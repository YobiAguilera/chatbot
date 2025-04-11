using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataFrecuenciaPedidos
    {
        public string proveedor { get; set; }
        public string articulo { get; set; }
        public string ordenesEnviadas { get; set; }
        public decimal costoTotalOrdenes { get; set; }

        public string columnaDinamica
        {
            get { return string.IsNullOrEmpty(proveedor) ? articulo : proveedor;  }
        }

    }
}
