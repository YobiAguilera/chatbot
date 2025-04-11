using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class dataReporteGeneralMantenimiento
    {
        public string placas { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string serie { get; set; }
        public string solicitudes { get; set; }
        public decimal costoTotal { get; set; }
        public string sucursal { get; set; }
        public DateTime fecha { get; set; }
    }
}
