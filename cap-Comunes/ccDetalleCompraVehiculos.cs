using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cap_Comunes
{
    public class ccDetalleCompraVehiculos
    {
        public int id { get; set; }
        public string concepto { get; set; }
        public double costo { get; set; }
        public int cantidad { get; set; }
        public string marca { get; set; }
        public double subtotal { get; set; }
        public string folioSol { get; set; }
        public DateTime fechaAltaSql { get; set; }
    }
}
