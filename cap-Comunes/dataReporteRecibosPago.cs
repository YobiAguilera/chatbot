using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataReporteRecibosPago
    {
        public string acreedor { get; set; }
        public string fecha { get; set; }
        public string firmaUrl { get; set; }
        public string folio { get; set; }
        public string metodo { get; set; }
        public string monto { get; set; }
        public string numeroMovimiento { get; set; }
        public string tipoOrden { get; set; }
        public string usuario { get; set; }
        public string sucursal { get; set; }
        public string detalle { get; set; }
    }
}
